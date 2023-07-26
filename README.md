# WinProcessMonitor
This utility is mostly meant as of [Veeam Software](https://www.veeam.com) recruiting process. This program monitors a process that has been specified using a command line argument and kills the process, if it has lived for a specified certain amount of time (in minutes).
# Usage
Open command prompt after [publishing the application](https://learn.microsoft.com/en-us/dotnet/core/tutorials/publishing-with-visual-studio?pivots=dotnet-6-0) and open the .exe path.

**Run the app by using the executable**: `WinProcessMonitor.exe [process name] [maximum length] [frequency]`

**Run the app by using the dotnet command**: `dotnet WinProcessMonitor.dll [process name] [maximum length] [frequency]`

_an example by using the executable_: `WinProcessMonitor.exe notepad 5 1`
