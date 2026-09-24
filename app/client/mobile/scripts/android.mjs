import { select } from "@inquirer/prompts";
import { spawn, execFileSync } from "node:child_process";
import fs from "node:fs";
import path from "node:path";
import { exit } from "node:process";
import { fileURLToPath } from "node:url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const root = path.resolve(__dirname, "..");

const platforms = {
  win32: {
    launchTerminal: startWindowsTerminal,
    killProcess: killWindowsProcess,
    findPortPid: findPortPidWindows
  },

  darwin: {
    launchTerminal: startMacTerminal,
    killProcess: killUnixProcess,
    findPortPid: findPortPidUnix
  },

  linux: {
    launchTerminal: startLinuxTerminal,
    killProcess: killUnixProcess,
    findPortPid: findPortPidUnix
  }
};

const EXPO_PORT = 8081;
let metroProcess = null;
let selectedEnvironment = null;
// ---------------------------------------------------------
// Environment
// ---------------------------------------------------------

async function selectEnvironment() {
  return select({
    message: "Select environment",
    choices: [
      {
        name: "Development",
        value: "development",
        description: "Local development environment"
      },
      {
        name: "Staging",
        value: "staging",
        description: "Staging backend"
      },
      {
        name: "Production",
        value: "production",
        description: "Production backend"
      },
      {
        name: "Exit",
        value: "exit",
        description: "terminate bash process and this process"
      }
    ]
  });
}

function handleInput(environment) {
  if (selectedEnvironment === environment) return;
  if (environment === "exit") {
    killExpoDev();
    exit();
  }

  let fileEx = "";
  if (environment !== "development") fileEx = `.${environment}`;

  const source = path.join(root, `.env${fileEx}`);
  const target = path.join(root, ".env.local");

  if (!fs.existsSync(source)) {
    console.error(`\nEnvironment file not found: ${source}`);
    return false;
  }

  fs.copyFileSync(source, target);

  selectedEnvironment = environment;

  console.log(`\nEnvironment: ${environment}`);

  return true;
}

// ---------------------------------------------------------
// Start Expo
// ---------------------------------------------------------

function startExpo() {
  console.log("\nStarting Expo...\n");

  const script = path.join(root, "scripts", "expo-dev.sh");

  const platform = platforms[process.platform];

  if (!platform) {
    throw new Error(`Unsupported platform: ${process.platform}`);
  }

  killExpo();
  metroProcess = platform.launchTerminal(script);
}

function startWindowsTerminal(script) {
  const process = spawn(
    "C:\\Program Files\\Git\\git-bash.exe",
    ["--cd=" + root, "-c", `bash "${script}"; exec bash`],
    {
      detached: true,
      stdio: "ignore"
    }
  );

  process.unref();

  return process;
}

function startMacTerminal(script) {
  const process = spawn(
    "osascript",
    ["-e", `tell application "Terminal" to do script "cd '${root}' && bash '${script}'"`],
    {
      detached: true,
      stdio: "ignore"
    }
  );

  process.unref();

  return process;
}

function startLinuxTerminal(script) {
  const process = spawn("x-terminal-emulator", ["-e", `bash "${script}"`], {
    detached: true,
    stdio: "ignore"
  });

  process.unref();

  return process;
}

// ---------------------------------------------------------
// Find process id
// ---------------------------------------------------------

function findPortPid(port) {
  const platform = platforms[process.platform];

  if (!platform) {
    throw new Error(`Unsupported platform: ${process.platform}`);
  }

  return platform.findPortPid(port);
}

function findPortPidWindows(port) {
  try {
    const output = execFileSync("netstat", ["-ano"], { encoding: "utf8" });

    const line = output
      .split("\n")
      .find(line => line.includes(`:${port}`) && line.includes("LISTENING"));

    if (!line) {
      return null;
    }

    return Number(line.trim().split(/\s+/).pop());
  } catch {
    return null;
  }
}

function findPortPidUnix(port) {
  try {
    const output = execFileSync("lsof", ["-ti", `:${port}`], { encoding: "utf8" });

    const pid = Number(output.trim().split("\n")[0]);

    return Number.isNaN(pid) ? null : pid;
  } catch {
    return null;
  }
}

// ---------------------------------------------------------
// Terminate process id
// ---------------------------------------------------------

function killExpo() {
  const pid = findPortPid(EXPO_PORT);

  if (!pid) {
    console.log("Expo is not running");
    return;
  }

  console.log(`Killing Expo: ${pid}`);

  try {
    const platform = platforms[process.platform];

    if (!platform) {
      throw new Error(`Unsupported platform: ${process.platform}`);
    }

    platform.killProcess(pid);
  } catch {}
}

function killExpoDev() {
  if (!metroProcess || metroProcess.killed) return;

  const platform = platforms[process.platform];

  if (!platform) {
    throw new Error(`Unsupported platform: ${process.platform}`);
  }
  platform.killProcess(metroProcess.pid);
}

function killWindowsProcess(pid) {
  try {
    execFileSync("taskkill", ["/PID", String(pid), "/T", "/F"]);
  } catch {}
}

function killUnixProcess(pid) {
  try {
    process.kill(pid, "SIGTERM");
  } catch {}
}

// ---------------------------------------------------------
// Reload
// ---------------------------------------------------------

function reloadExpo() {
  console.log("Restarting Expo...");

  killExpo();
}

// ---------------------------------------------------------
// Menu
// ---------------------------------------------------------

async function showMenu() {
  handleInput(await selectEnvironment());
  if (!metroProcess?.pid) {
    startExpo();
  } else {
    reloadExpo();
  }

  setTimeout(showMenu, 500);
}

// ---------------------------------------------------------
// Start
// ---------------------------------------------------------

showMenu();
