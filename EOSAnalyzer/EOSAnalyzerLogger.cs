using System.IO;

namespace EOSAnalyzer
{
    public static class EOSAnalyzerLogger
    {
        public const string LoggerFile = "EOSAnalyzerLogger.log";
        private const string LoggerTitle = "Explanatory Observer Solusion VSIX Extension @Type29 2024-2025\r\n";
        private static readonly string VSIXPath = @"D:\";
        private static bool Init = false;
        private static string fullPath => Path.Combine(VSIXPath, LoggerFile);

        public static void Log(string log)
        {
            if (!Init)
            {
                Init = true;

                //File.WriteAllText(fullPath, LoggerTitle);
            }
            //File.AppendAllText(fullPath, $"[{DateTime.Now}][Log]" + log + "\r\n");
        }
    }
}