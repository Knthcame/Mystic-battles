using Prism.Logging;

namespace PVPMistico.Logging.Interfaces
{
    public interface ICustomLogger : ILoggerFacade
    {
        void Log(string message, object obj, Category category, Priority priority);

        void Debug(string message, object obj = null, Priority priority = Priority.Low);

        void Warn(string message, object obj = null, Priority priority = Priority.Medium);

        void Error(string message, object obj = null, Priority priority = Priority.High);

        void Info(string message, object obj = null, Priority priority = Priority.None);
    }
}
