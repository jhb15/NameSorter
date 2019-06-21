using System;
using System.Collections.Generic;
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
        private static List<string> _filePaths = null;
        private static bool _ascending = true;

        private static void ExtractArgs(string[] args)
        {
            var formatIndex = -1;
            _filePaths = new List<string>();
            foreach ((string value, int i) in args.Select((value, i) => (value, i)))
            {
                if (value.StartsWith("-"))
                {
                    switch (value.Substring(1))
                    {
                        case "a":
                            _ascending = true;
                            break;
                        case "d":
                            _ascending = false;
                            break;
                        case "f":
                            formatIndex = i + 1;
                            break;
                        default:
                            Console.WriteLine("Error Unrecognised Flag, the Flag \"" + value + "\" is going to be ignored!");
                            break;
                    }
                }
                else
                {
                    if (i == formatIndex)
                    {
                        _format = StringToNameFormat(value);
                    }
                    else
                    {
                        _filePaths.Add(value);
                    }
                }
            }
        }

        private static NameFormats StringToNameFormat(string formatStr)
        {
            var parseRet = Enum.TryParse(formatStr, true, out NameFormats outFormat);
                
            if (!parseRet)
            {
                var possibleFormats = string.Join(",", Enum.GetNames(typeof(NameFormats)));
                throw new ArgumentException("Inputted Format Not Recognised! got: \"" + formatStr + "\" possible values: \"" + possibleFormats + "\"");
            }

            return outFormat;
        }
        
        /*private static void ExtractArgs(string[] args)
        {
            var numberOfInputs = args.Length;
            var indexOfFormat = Array.FindIndex(args, s => s.Equals("-f"));
            var indexOfAscending = Array.FindIndex(args, s => s.Equals("-a"));
            var indexOfDescending = Array.FindIndex(args, s => s.Equals("-d"));

            // Making assumption that if descending flag isn't found we will always use ascending. But we are still
            // looking for the -a flag as well just in case it is specified and we need to ignore it.
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
        }*/

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