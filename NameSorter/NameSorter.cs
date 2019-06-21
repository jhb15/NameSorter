using System;
using System.IO;
using System.Linq;
using System.Net;
using NameSorter.Interfaces;

namespace NameSorter
{
    public class NameSorter
    {
        private readonly IFileWrapper _fileWrapper;

        /**
         * Constructs a name sorter object and also tales in a file wrapper object, this is to make the class unit
         * testable.
         */
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
         * This function is used to output the sorted names to the command line and also a text file in the current
         * directory.
         */
        private void OutputToFile(string inputPath, Name[] sortedNames)
        {
            var filePathParts = inputPath.Split('/');
            var fileName = filePathParts[filePathParts.Length - 1];
            var sortedFilename = "sorted" + fileName;
            Console.WriteLine("input file name: " + fileName);
            using (var file =
                _fileWrapper.GetStreamWriter(sortedFilename))
            {
                Console.WriteLine("Sorted Names for File \"" + inputPath + "\":");
                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                    file.WriteLine(name.ToString());
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("Output File Name: " + sortedFilename);
        }

        /**
         * This method is used to sort a list of names contained in a .txt file. It takes a file path and the format that
         * corresponds to the format of the names within the file.
         */
        public void Sort(string path, NameFormats format, bool isAscending)
        {
            var names = ExtractNames(path, format);
            
            if (names == null) return;
            
            names = isAscending ? names.OrderBy(s => s.Forenames).ThenBy(s => s.Surname).ToArray() : 
                names.OrderByDescending(s => s.Surname).ThenByDescending(s => s.Forenames).ToArray();
            
            OutputToFile(path, names);
        }
    }
}