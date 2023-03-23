namespace ToH.Log;

public interface ILog
{
    void LogDebug(string line);
    void LogInfo(string line);
    void LogWarn(string line);
    void LogError(string line);
}