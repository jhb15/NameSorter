using System.IO;

namespace NameSorter.Interfaces
{
    public interface IFileWrapper
    {
        string[] ReadAllLines(string filePath);

        StreamWriter GetStreamWriter(string path);
    }
}