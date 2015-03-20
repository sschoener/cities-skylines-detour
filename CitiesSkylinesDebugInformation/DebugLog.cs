using System.IO;
using ColossalFramework.Plugins;

namespace CitiesSkylinesDetour
{
    public static class DebugLog
    {
        private static StreamWriter _logFile;

        public static void Init()
        {
            _logFile = File.CreateText("C:\\log.txt");
            _logFile.AutoFlush = true;
        }

        public static void Close()
        {
            _logFile.Close();
        }

        public static void LogToFileOnly(string msg)
        {
            _logFile.WriteLine(msg);
        }

        public static void Log(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, msg);
            _logFile.WriteLine(msg);
        }

        public static void LogWarning(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Warning, msg);
            _logFile.WriteLine("Warning: " + msg);
        }

        public static void LogError(string msg)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, msg);
            _logFile.WriteLine("Error: " + msg);
        }
    }
}