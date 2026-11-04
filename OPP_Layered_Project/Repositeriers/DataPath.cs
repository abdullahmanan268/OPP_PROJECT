using System;
using System.IO;

namespace LostAndFoundCSharp.Repositories
{
    
    public static class DataPath
    {
        public static string GetFile(string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null)
            {
                string path = Path.Combine(dir.FullName, "Data", fileName);
                if (File.Exists(path))
                {
                    return path;
                }
                dir = dir.Parent;
            }

            
            string newFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            Directory.CreateDirectory(newFolder);
            string newPath = Path.Combine(newFolder, fileName);
            Console.WriteLine("WARNING: " + fileName + " not found. A new empty file will be used: " + newPath);
            return newPath;
        }
    }
}
