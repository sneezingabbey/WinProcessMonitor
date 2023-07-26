using System.Diagnostics;

namespace WinProcessMonitor;

public class ProcessHelper
{
    private readonly int _frequency;
    private readonly int _maximumLifetime;
    private readonly string _processName;

    public ProcessHelper(string processName, int maximumLifetime, int frequency)
    {
        _processName = processName;
        _maximumLifetime = maximumLifetime;
        _frequency = frequency;
    }

    public void CheckProcess()
    {
        foreach (var process in Process.GetProcessesByName(_processName))
        {
            var runtime = DateTime.Now - process.StartTime;

#if DEBUG
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(
                $"Runtime (minutes): {runtime.Minutes}, StartTime: {process.StartTime}, CurrentTime: {DateTime.Now}");
#endif

            if (_maximumLifetime <= 0) throw new ArgumentOutOfRangeException(nameof(_maximumLifetime));

            if (runtime.Minutes > _maximumLifetime)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    $"A process called {process.ProcessName} has been running for {runtime.Minutes} minutes, which exceeds maximum lifetime of {_maximumLifetime} minutes");
                process.Kill();
                process.Dispose();
            }
        }
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