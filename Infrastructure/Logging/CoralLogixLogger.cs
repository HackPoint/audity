using Application.Interfaces;
using CoralogixCoreSDK;

namespace Infrastructure.Logging;

public class CoralLogixLogger<T> : ICoralogixLogger<T> {
    private readonly CoralogixLogger _logger;

    public CoralLogixLogger() {
        _logger = CoralogixLogger.GetLogger("arborknot");
        _logger.Configure(Environment.GetEnvironmentVariable("CORALOGIX_PRIVATE_KEY"), "Audit", "infra");
    }

    public void Info(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Info, message, category, className, methodName, threadId);
    }

    public void Warning(
        string message,
        string category = "",
        string className = "",
        string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Warning, message, category, className, methodName, threadId);
    }

    public void Verbose(string message, string category = "", string className = "", string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Verbose, message, category, className, methodName, threadId);
    }

    public void Error(string message, string category = "", string className = "", string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Error, message, category, className, methodName, threadId);
    }

    public void Critical(string message, string category = "", string className = "", string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Critical, message, category, className, methodName, threadId);
    }

    public void Debug(string message, string category = "", string className = "", string methodName = "",
        string threadId = "") {
        _logger.Log(Severity.Debug, message, category, className, methodName, threadId);
    }
}