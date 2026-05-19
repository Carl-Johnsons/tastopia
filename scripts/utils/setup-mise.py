#!/usr/bin/env python3

import argparse
import os
import platform
import shlex
import shutil
import subprocess
import sys
from enum import StrEnum
from http.client import HTTPResponse
from pathlib import Path
from urllib.request import urlopen


class LinuxDistribution(StrEnum):
    ARCH = "arch"
    UNSUPPORTED = "unsupported"

    @classmethod
    def _missing_(cls, value: object):
        return cls.UNSUPPORTED


def get_linux_distribution() -> LinuxDistribution:
    with open("/etc/os-release") as file:
        for line in file:
            key, value = line.rstrip().split("=", 1)

            if key == "ID":
                return LinuxDistribution(value)

    return LinuxDistribution.UNSUPPORTED


def resolve_home_dir() -> Path:
    sudo_user = os.environ.get("SUDO_USER")

    if sudo_user:
        try:
            import pwd

            return Path(pwd.getpwnam(sudo_user).pw_dir)
        except (ImportError, KeyError):
            pass

    return Path.home()


def resolve_shell_name() -> str:
    shell_path = os.environ.get("SHELL")
    sudo_user = os.environ.get("SUDO_USER")

    if sudo_user:
        try:
            import pwd

            user_shell = pwd.getpwnam(sudo_user).pw_shell
            if user_shell:
                shell_path = user_shell
        except (ImportError, KeyError):
            pass

    if shell_path:
        return Path(shell_path).name

    if platform.system() == "Windows":
        if shutil.which("pwsh") or shutil.which("powershell"):
            return "pwsh"

        raise RuntimeError("PowerShell is not found")

    raise RuntimeError("Cannot detect shell: SHELL is not set")


def ensure_root():
    if os.geteuid() != 0:
        os.execvp("sudo", ["sudo", sys.executable, *sys.argv])


def fetch_http(url: str) -> str:
    with urlopen(url) as response:
        response: HTTPResponse

        if response.status != 200:
            raise RuntimeError(
                f"Request failed while fetching installation script: {response.status}"
            )

        return response.read().decode()


def install_for_linux_generic():
    shell = resolve_shell_name()

    match shell:
        case "bash" | "zsh" | "fish":
            url = f"https://mise.run/{shell}"
            script = fetch_http(url)
            subprocess.run(["sh"], input=script, check=True, text=True)

        case _:
            raise RuntimeError(f"Unsupported shell: {shell}")


def install_for_linux():
    linux_distribution = get_linux_distribution()

    match linux_distribution:
        case LinuxDistribution.ARCH:
            ensure_root()
            subprocess.run(
                ["pacman", "-S", "--needed", "--noconfirm", "mise"], check=True
            )
        case LinuxDistribution.UNSUPPORTED:
            install_for_linux_generic()


def install_for_windows():
    if shutil.which("winget") is None:
        raise RuntimeError("winget is not found")

    subprocess.run(
        [
            "winget",
            "install",
            "--id",
            "jdx.mise",
            "--exact",
            "--silent",
            "--accept-package-agreements",
            "--accept-source-agreements",
        ],
        check=True,
    )


def install_for_macos():
    if shutil.which("brew") is None:
        raise RuntimeError("Homebrew is not found")

    subprocess.run(
        [
            "brew",
            "install",
            "mise",
        ],
        check=True,
    )


def install():
    os_name = platform.system()

    match os_name:
        case "Linux":
            install_for_linux()

        case "Windows":
            install_for_windows()

        case "Darwin":
            install_for_macos()

        case _:
            raise RuntimeError(f"Unsupported operating system: {os_name}")


def install_dependencies():
    mise_path = resolve_mise_path(resolve_home_dir())
    subprocess.run([mise_path, "install"], check=True)


def resolve_mise_path(home_dir: Path) -> Path:
    mise_path = shutil.which("mise")
    if mise_path:
        return Path(mise_path)

    fallback = home_dir / ".local" / "bin" / "mise"
    if fallback.exists():
        return fallback

    raise RuntimeError("mise executable not found after installation")


def resolve_powershell_profile() -> Path:
    for command in ("pwsh", "powershell"):
        if shutil.which(command) is None:
            continue

        profile_path = subprocess.run(
            [command, "-NoProfile", "-Command", "$PROFILE"],
            check=True,
            capture_output=True,
            text=True,
        ).stdout.strip()

        if not profile_path:
            return Path(profile_path)

    raise RuntimeError("PowerShell is not found")


def format_powershell_path(path: Path) -> str:
    escaped = str(path).replace("'", "''")
    return f"'{escaped}'"


def is_windows_git_bash(shell: str) -> bool:
    os_name = platform.system()

    return shell == "bash" and (
        os_name == "Windows"
        or os_name.startswith(("MINGW", "MSYS_NT"))
        or os.environ.get("OSTYPE") == "msys"
        or os.environ.get("MSYSTEM") is not None
    )


def format_activation_line(shell: str, mise_path: Path) -> str:
    if is_windows_git_bash(shell):
        return (
            'eval "$(mise activate bash | sed \''
            's|eval "$(mise hook-env -s bash)";|'
            '& export PATH="$(/usr/bin/cygpath -u -p "$PATH")";|'
            '\')"'
        )

    if shell == "fish":
        return f"{shlex.quote(str(mise_path))} activate fish | source"

    if shell == "pwsh":
        return (
            f"(&{format_powershell_path(mise_path)} activate pwsh) | "
            "Out-String | Invoke-Expression"
        )

    return f'eval "$({shlex.quote(str(mise_path))} activate {shell})"'


def resolve_shell_config_file(shell: str, home_dir: Path) -> Path:
    match shell:
        case "bash":
            return home_dir / ".bashrc"
        case "zsh":
            return home_dir / ".zshrc"
        case "fish":
            return home_dir / ".config" / "fish" / "config.fish"
        case "pwsh":
            return resolve_powershell_profile()
        case _:
            raise RuntimeError(f"Unsupported shell for activation: {shell}")


def ensure_activation():
    shell = resolve_shell_name()
    home_dir = resolve_home_dir()
    mise_path = resolve_mise_path(home_dir)
    activation_line = format_activation_line(shell, mise_path)
    shell_config_path = resolve_shell_config_file(shell, home_dir)
    shell_config_content = (
        shell_config_path.read_text(encoding="utf-8")
        if shell_config_path.exists()
        else ""
    )

    if activation_line in shell_config_content:
        print(f"mise activation already configured in {shell_config_path}")
        return

    shell_config_path.parent.mkdir(parents=True, exist_ok=True)
    with shell_config_path.open("a", encoding="utf-8") as file:
        if shell_config_content and not shell_config_content.endswith("\n"):
            file.write("\n")
        file.write(f"{activation_line}\n")

    print(f"mise activation added to {shell_config_path}")
    print("Restart your shell session to apply mise activation")


class Args(argparse.Namespace):
    install_dependencies: bool


def parse_args() -> Args:
    parser = argparse.ArgumentParser(
        prog="setup-mise.py",
        description=(
            "Install mise, configure shell activation, and optionally set up "
            "the project dependencies defined in mise.toml."
        ),
    )

    parser.add_argument(
        "--install-dependencies",
        action="store_true",
        help=(
            "Run 'mise install' after mise has been installed and shell "
            "activation has been configured, setting up the project toolchain "
            "from mise.toml."
        ),
    )

    return parser.parse_args(namespace=Args())


def main() -> int:
    args = parse_args()

    try:
        if shutil.which("mise"):
            print("mise is already installed. Skipping install")
        else:
            install()
            print("mise is installed successfully")
        ensure_activation()

        if args.install_dependencies:
            install_dependencies()
            print("mise dependencies are installed successfully")

        return 0
    except subprocess.CalledProcessError as error:
        print(f"error: command failed: {error.cmd}")
        return error.returncode
    except Exception as error:
        print(f"error: {error}")
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
