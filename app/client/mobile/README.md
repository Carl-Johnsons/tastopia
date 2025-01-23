
## WINDOW ISSUE:
Some library will find trouble to the library if the path is too long when trying to run ```npx expo run:android```

- Run this command to enable long path in <b>powershell</b> as <b><i> Administrator </i></b>
``` Powershell
New-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Control\FileSystem" -Name "LongPathsEnabled" -Value 1 -PropertyType DWORD -Force
```

- And enable git long path too
```
git config --system core.longpaths true
```

Ref:
- [Build failure in window 10 github issue](https://github.com/software-mansion/react-native-reanimated/issues/6224)
- [How to disable Maximum Path Length Limitation](https://learn.microsoft.com/en-us/windows/win32/fileio/maximum-file-path-limitation?tabs=powershell)
- [File name too long during patch npm package](https://github.com/ds300/patch-package/issues/156)