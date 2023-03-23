namespace ToH.Log;

public class FileLog : ILog, IDisposable
{
    private readonly StreamWriter _file;

    public FileLog(string filepath, bool append)
    {
        _file = new StreamWriter(filepath, append)
        {
            AutoFlush = true
        };

        LogInfo("Logger initialized.");
    }

    private void Log(string level, string line)
    {
        _file.WriteLine($"{DateTime.Now} - {level} - {line}");
    }

    public void LogDebug(string line)
    {
        Log("DEBUG", line);
    }

    public void LogError(string line)
    {
        Log("ERROR", line);
    }

    public void LogInfo(string line)
    {
        Log("INFO", line);
    }

    public void LogWarn(string line)
    {
        Log("WARN", line);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _file.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}