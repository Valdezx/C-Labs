using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Utils
{
    public class ClinicLogger
    {
        private readonly string _logPath;

        public ClinicLogger(string logPath = "clinic.log")
        {
            _logPath = string.IsNullOrEmpty(logPath) ? "clinic.log" : logPath;
        }
        public void LogInfo (string message)
        {
            WriteLog("INFO", message);
        }
        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }
        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }
        public string[] GetLastLines(int n)
        {
            if (n <= 0 || !File.Exists(_logPath))
            {
                return Array.Empty<string>();
            }

            string[] allLines = File.ReadAllLines(_logPath);
            int count = allLines.Length < n ? allLines.Length : n;
            string[] result = new string[count];

            int startIndex = allLines.Length - count;
            for (int i = 0; i < count; i++)
            {
                result[i] = allLines[startIndex + i];
            }

            return result;
        }
        public void Clear()
        {
            if (File.Exists(_logPath))
            {
                File.WriteAllText(_logPath, string.Empty);
            }
        }
        public bool Exists()
        {
            return File.Exists(_logPath);
        }
        private void WriteLog(string level, string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            File.AppendAllText(_logPath, logEntry + Environment.NewLine);
        }
    }
}
