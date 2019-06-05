using System.IO;

namespace NameSorter.Interfaces
{
    public class FileWrapper:IFileWrapper
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public StreamWriter GetStreamWriter(string path)
        {
            return new StreamWriter(path);
        }
    }
}