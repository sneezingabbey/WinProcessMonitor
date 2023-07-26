namespace WinProcessMonitor;

internal static class Program
{
    private static ProcessHelper? _processHelper;
    private static Task? _frequencyAsyncTask;

    public static void Main(string[] args)
    {
        WriteLine(
            "This utility monitors a command line specified process and kills it, if it has exceeded specified time running");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("First argument: a process name to be monitored");
        Console.WriteLine("Second argument: its maximum lifetime (in minutes)");
        Console.WriteLine("Third argument: a monitoring frequency (in minutes)\n");

        if (args == null || args.Length == 0)
        {
            throw new ArgumentNullException(nameof(args));
        }

        _processHelper = new ProcessHelper(args[0], int.Parse(args[1]), int.Parse(args[2]));
        WriteLine("ProcessName: " + _processHelper.GetProcessName());
        WriteLine("Lifetime: " + _processHelper.GetMaximumLifetime());
        WriteLine("Frequency: " + _processHelper.GetFrequency());

        _frequencyAsyncTask = FrequencyAsyncTask(_processHelper);

        Console.WriteLine();

        WriteLine("Press q to exit the program");

        Console.WriteLine();

        if (Console.ReadKey().KeyChar != 113) return;

        _frequencyAsyncTask?.Dispose();
        Environment.Exit(0);
    }

    private static async Task FrequencyAsyncTask(ProcessHelper? processHelper)
    {
        if (processHelper == null) throw new ArgumentNullException(nameof(processHelper));

        var frequency = processHelper.GetFrequency();
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(frequency));

        while (await timer.WaitForNextTickAsync()) processHelper.CheckProcess();
    }

    private static void WriteLine(string message)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);
    }
}