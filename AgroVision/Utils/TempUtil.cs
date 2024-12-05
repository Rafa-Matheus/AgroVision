using System.IO;

namespace AgroVision.Utils
{
    public class TempUtil
    {

        private static readonly string TEMP_PATH = @".\temp\";
        public static void CreateTempDirectoryIfNotExists()
        {
            if (!Directory.Exists(TEMP_PATH))
                Directory.CreateDirectory(TEMP_PATH);
        }

        public static bool FileExists(string fileName)
        {
            return File.Exists(GetTempFilePath(fileName));
        }

        public static void DeleteTempFile(string fileName)
        {
            string filePath = GetTempFilePath(fileName);
            if (FileExists(filePath))
                File.Delete(filePath);
        }

        public static string GetTempFilePath(string fileName)
        {
            return $"{TEMP_PATH}{fileName}";
        }

        public static void ClearTemp()
        {
            if (Directory.Exists(TEMP_PATH))
                Directory.Delete(TEMP_PATH, true);
        }

    }
}