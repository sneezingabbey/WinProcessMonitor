using System.Diagnostics;

namespace WinProcessMonitor.NUnitTests;

public class ProcessHelperTests
{
    private static ProcessHelper _processHelper = null!;
    private static ProcessHelper _processHelperNull = null!;

    [SetUp]
    public void Setup()
    {
        // ARRANGE
        _processHelperNull = new ProcessHelper(string.Empty, 0, 0);
    }


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        // ARRANGE
        _processHelper = new ProcessHelper("notepad", 5, 1);
        CreateProcess(_processHelper.GetProcessName());
    }

    [Test]
    public void GetProcessName_Throws_NullException()
    {
        // ASSERT
        Assert.Throws<ArgumentNullException>(() => _processHelperNull.GetProcessName());
    }

    [Test]
    public void GetFrequency_Throws_OutOfRangeException()
    {
        // ASSERT
        Assert.Throws<ArgumentOutOfRangeException>(() => _processHelperNull.GetFrequency());
    }

    [Test]
    public void GetMaximumLifetime_Throws_OutOfRangeException()
    {
        // ASSERT
        Assert.Throws<ArgumentOutOfRangeException>(() => _processHelperNull.GetMaximumLifetime());
    }

    [Test]
    public void FindProcess_Found_ReturnsProcess()
    {
        // ACT
        var processName = _processHelper.GetProcessName().ToLower();
        var foundProcess = _processHelper.FindProcess(processName);

        // ASSERT
        if (foundProcess == null)
        {
            Assert.Fail("found process is null");
            return;
        }

        var foundProcName = foundProcess.ProcessName.ToLower();

        Assert.That(processName, Is.EqualTo(foundProcName));
    }

    [Test]
    public void CheckProcess_Throws_NullException()
    {
        // ASSERT
        Assert.Throws<ArgumentNullException>(() => _processHelperNull.CheckProcess());
    }

    [Test]
    public void CheckProcess_Throws_OutOfRangeException()
    {
        // ARRANGE + ACT
        var processHelper = new ProcessHelper("notepad", 0, 0);

        // ASSERT
        Assert.Throws<ArgumentOutOfRangeException>(() => processHelper.CheckProcess());
    }

    private static void CreateProcess(string name)
    {
        var process = new Process();
        process.StartInfo.FileName = $"{name}.exe";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
        process.Start();
    }
}