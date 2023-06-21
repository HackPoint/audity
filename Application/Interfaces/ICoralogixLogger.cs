namespace Application.Interfaces;

public interface ICoralogixLogger<T> {
    void Info(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");


    public void Warning(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");

    public void Verbose(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");

    public void Error(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");

    public void Critical(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");

    public void Debug(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "");
}