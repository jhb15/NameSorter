using System;
using System.IO;
using System.Net;
using NameSorter.Interfaces;

namespace NameSorter
{
    public class NameSorter
    {
        private IFileWrapper _fileWrapper;

        public NameSorter(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }
        
        private Name[] ExtractNames(string filePath, NameFormats format)
        {
            try
            {
                var fileLines = _fileWrapper.ReadAllLines(filePath);
                var names = new Name[fileLines.Length];
                Console.WriteLine("Names from File \"" + filePath + "\":");
                for(var i = 0; i < fileLines.Length; i++)
                {
                    names[i] = new Name(fileLines[i], format);
                    Console.WriteLine(names[i]);
                }
                Console.WriteLine("\n");
                return names;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File Not Found Exception. Msg: " + e.Message);
                return null;
            }
        }

        private void SortNames(Name[] unsortedNames)
        {
            SortRecursive(unsortedNames, 0, unsortedNames.Length-1);
        }

        private int Partition(Name[] names, int left, int right)
        {
            var pivot = names[left];
            while (true)
            {
                while (names[left].CompareTo(pivot) < 0)
                    left++;
 
                while (names[right].CompareTo(pivot) > 0)
                    right--;
 
                if (left < right)
                {
                    var temp = names[right];
                    names[right] = names[left];
                    names[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        private void SortRecursive(Name[] names, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(names, left, right);

                if (pivot > 1)
                    SortRecursive(names, left, pivot - 1);

                if (pivot + 1 < right)
                    SortRecursive(names, pivot + 1, right);
            }
        }

        private void OutputToFile(string inputPath, Name[] sortedNames)
        {
            var filePathParts = inputPath.Split('/');
            var fileName = filePathParts[filePathParts.Length - 1];
            Console.WriteLine("input file name: " + fileName);
            using (var file =
                _fileWrapper.GetStreamWriter(@"sorted" + fileName))
            {
                Console.WriteLine("Sorted Names for File \"" + inputPath + "\":");
                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                    file.WriteLine(name.ToString());
                }
                Console.WriteLine("\n");
            }
        }

        public void Sort(string path, NameFormats format)
        {
            var names = ExtractNames(path, format);
            SortNames(names);
            OutputToFile(path, names);
        }
    }
}