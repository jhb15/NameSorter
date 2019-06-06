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
        
        /**
         * This function takes the inputted filepath and extracts the names from this file returning it as a Name object
         * array.
         */
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

        /**
         * This function is used to trigger the recursive sort algorithm.
         */
        private void SortNames(Name[] unsortedNames)
        {
            SortRecursive(unsortedNames, 0, unsortedNames.Length-1);
        }

        /**
         * This is part of a solution for sorting the names, it was modified from an example found online. Here is a URL
         * to the original code: https://www.csharpstar.com/csharp-program-quick-sort/
         */
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

        /**
         * This is the recursive function that forms part of the solution and is also adapted from the code found here:
         * https://www.csharpstar.com/csharp-program-quick-sort/
         */
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

        /**
         * This function is used to output the sorted names to the command line and also a text file in the current
         * directory.
         */
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

        /**
         * This method is used to sort a list of names contained in a .txt file. It takes a file path and the format that
         * corresponds to the format of the names within the file.
         */
        public void Sort(string path, NameFormats format)
        {
            var names = ExtractNames(path, format);
            SortNames(names);
            OutputToFile(path, names);
        }
    }
}