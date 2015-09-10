using System;
using System.IO;

namespace TestSiteParser.Utilities
{
    internal static class MyLogger
    {

        private static readonly string DirectoryName = Path.DirectorySeparatorChar + "Logs";

        public static void Log(string data)
        {
            try
            {
                var rootDirectory = Directory.GetCurrentDirectory();
                var newDirectory = Directory.CreateDirectory(rootDirectory + DirectoryName);
                var filename = Path.DirectorySeparatorChar + string.Format("{0:yyyy-MM-dd HH-mm-ss}.txt", DateTime.Now);
                using (var newFile = File.Create(newDirectory + filename))
                {
                    using (StreamWriter streamWriter = new StreamWriter(newFile))
                    {
                        streamWriter.Write(data);
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
