using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Infrastructure.FileSystem
{
    public class FileSystemService
    {
        public void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public string GetApplicationDataPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TempChecker");
        }

        public void WriteAllTextSafe(string path, string content)
        {
            string tempPath = path + ".tmp";
            File.WriteAllText(tempPath, content);
            File.Move(tempPath, path, true);
        }
    }
}