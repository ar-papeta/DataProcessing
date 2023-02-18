using NLog;

namespace DataProcessing.Logger
{
    public static class Log
    {
        static Log()
        {
            var logger = LogManager.Setup().LoadConfiguration(builder => 
            {
                if (ConfigHelper.LogType == "Console")
                    builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
                else if (ConfigHelper.LogType == "File")
                    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "file.txt");
                else 
                    throw new Exception("Wrong log type, check appsetting.json");
            });
        }
      
        public static void LogInfo(string message)
        {
            LogManager.GetCurrentClassLogger().Log(LogLevel.Info, message);
        }

        public static void LogError(string message)
        {
            LogManager.GetCurrentClassLogger().Log(LogLevel.Error, message);
        }

        public static void LogDebug(string message)
        {
            LogManager.GetCurrentClassLogger().Log(LogLevel.Debug, message);
        }
    }
}
