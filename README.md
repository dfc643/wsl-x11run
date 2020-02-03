X11Run
-----
X11Run - Run X11 program with WSL like win32 program.

### How to use
1. Copy all files to ```C:\Windows``` or ```C:\Windows\System32```
1. Edit configuration file ```x11run.exe.config```
1. Run ```x11run xterm``` to open XTerm (just a example)

### Command line
```bash
x11run <Linux X11 Program>
x11run <Linux X11 Program> <Parameter>
x11run <Linux X11 Program> <Parameter 1> <Parameter 2> ... <Parameter n>
```

### Examples
Open XFCE4-Thunar    
```
x11run thunar /tmp
```

### Warning!
1. Don't use X11Run to launch command-line program (ps, top, htop ...)
2. Before use X11Run, make sure WSL working fine.

## Developed by xRetia.