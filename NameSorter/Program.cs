using System;
using System.Linq;
using NameSorter.Interfaces;

namespace NameSorter
{
    /**
     * Enum used to represent possible formats for the input name string, example is given as string with the names
     * in order which will be used as default but this enum will be used to future proof the code to some extent even
     * allowing for addition of other formats.
     */
    public enum NameFormats
    {
        InOrder,
        SurnameFirst,
        Backwards
    }
    
    class Program
    {
        private static NameFormats _format;
        private static string[] _filePaths = null;
        private static bool _ascending = true;
        
        private static void ExtractArgs(string[] args)
        {
            var numberOfInputs = args.Length;
            var indexOfFormat = Array.FindIndex(args, s => s.Equals("-f"));
            var indexOfAscending = Array.FindIndex(args, s => s.Equals("-a"));
            var indexOfDescending = Array.FindIndex(args, s => s.Equals("-d"));

            if (indexOfDescending != -1) _ascending = false; 
            
            if (indexOfFormat != -1)
            {
                var formatStr = args[indexOfFormat + 1];
                var parseRet = Enum.TryParse(formatStr, true, out NameFormats outFormat);
                numberOfInputs = args.Length - 2;
                
                if (!parseRet)
                {
                    var possibleFormats = string.Join(",", Enum.GetNames(typeof(NameFormats)));
                    throw new ArgumentException("Inputted Format Not Recognised! got: \"" + formatStr + "\" possible values: \"" + possibleFormats + "\"");
                }

                _format = outFormat;
            }

            _filePaths = new string[numberOfInputs];
            Console.WriteLine("Format: " + _format);

            Console.WriteLine("Input Files:");
            var j = 0;
            for (var i = 0; i < args.Length; i++)
            {
                if (((i != indexOfFormat) && (i != indexOfFormat + 1)) ||
                    ((i != indexOfAscending) && (i != indexOfDescending)))
                {
                    _filePaths[j] = args[i];
                    Console.WriteLine(args[i]);
                    j++;
                }
            }
            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {
            var nameSorter = new NameSorter(new FileWrapper());
            
            try
            {
                ExtractArgs(args);

                foreach (var path in _filePaths)
                {
                    if (path != null) nameSorter.Sort(path, _format, _ascending);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Argument Error, " + e.Message);
            }
        }
    }
}