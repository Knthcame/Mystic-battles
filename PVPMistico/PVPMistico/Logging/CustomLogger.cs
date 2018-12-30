using System;
using Prism.Logging;
using Newtonsoft.Json;
using PVPMistico.Logging.Interfaces;

namespace PVPMistico.Logging
{
    public class CustomLogger : ICustomLogger
    {
        public void Debug(string message, object obj = null, Priority priority = Priority.Low)
        {
            Log(message, obj, Category.Debug, priority);
        }

        public void Error(string message, object obj = null, Priority priority = Priority.High)
        {
            Log(message, obj, Category.Exception, priority);
        }

        public void Info(string message, object obj = null, Priority priority = Priority.None)
        {
            Log(message, obj, Category.Info, priority);
        }

        public void Log(string message, Category category, Priority priority)
        {
            string messageToLog =
                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "          {1}: {2}. Priority: {3}. Timestamp:{0:u}.",
                    DateTime.Now,
                    category.ToString().ToUpperInvariant(),
                    message,
                    priority.ToString());

            Console.WriteLine(messageToLog);
        }

        public void Log(string message, object obj, Category category, Priority priority)
        {
            try
            {
                if (obj == null)
                    Log(message, category, priority);
                else
                    Log($"{message} {JsonConvert.SerializeObject(obj)}", category, priority);
            }
            catch (JsonSerializationException ex)
            {
                if (ex.Message.Contains("loop"))
                    Log($"{message} {JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}", category, priority);
                else
                    Log(message, category, priority);
            }
        }

        public void Warn(string message, object obj = null, Priority priority = Priority.Medium)
        {
            Log(message, obj, Category.Warn, priority);
        }
    }
}
