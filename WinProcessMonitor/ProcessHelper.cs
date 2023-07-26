using System.Diagnostics;

namespace WinProcessMonitor;

public class ProcessHelper
{
    private readonly int _frequency;
    private readonly int _maximumLifetime;
    private readonly string _processName;

    private Process? _process;

    public ProcessHelper(string processName, int maximumLifetime, int frequency)
    {
        _processName = processName;
        _maximumLifetime = maximumLifetime;
        _frequency = frequency;
    }

    public void CheckProcess()
    {
        _process = FindProcess(_processName);

        if (_process is null) throw new ArgumentNullException(nameof(_process));

        var runtime = DateTime.Now - _process.StartTime;

#if DEBUG
        Console.WriteLine(
            $"Runtime (minutes): {runtime.Minutes}, StartTime: {_process.StartTime}, CurrentTime: {DateTime.Now}");
#endif

        if (_maximumLifetime <= 0) throw new ArgumentOutOfRangeException(nameof(_maximumLifetime));

        if (runtime.Minutes <= _maximumLifetime) return;

        Console.WriteLine(
            $"A process called {_process.ProcessName} has been running for {runtime.Minutes} minutes, which exceeds maximum lifetime of {_maximumLifetime} minutes");
        _process.Kill();
    }

    public Process? FindProcess(string processName)
    {
        foreach (var process in Process.GetProcessesByName(processName)) _process = process;

        return _process;
    }

    public string GetProcessName()
    {
        if (string.IsNullOrEmpty(_processName)) throw new ArgumentNullException(nameof(_processName));

        return _processName;
    }

    public int GetMaximumLifetime()
    {
        if (_maximumLifetime <= 0) throw new ArgumentOutOfRangeException(nameof(_maximumLifetime));

        return _maximumLifetime;
    }

    public int GetFrequency()
    {
        if (_frequency <= 0) throw new ArgumentOutOfRangeException(nameof(_frequency));

        return _frequency;
    }
}