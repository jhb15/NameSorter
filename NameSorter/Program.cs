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

        /**
         * Function for extracting the arguments from the args input in the main function, it takes the input string
         * array directly and extracts the flags from it and also the fullpaths.
         */
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

        /**
         * Function for converting string into a format enum.
         */
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

        static void Main(string[] args)
        {
            var nameSorter = new NameSorter(new FileWrapper());
            
            try
            {
                ExtractArgs(args);

                foreach (var path in _filePaths)
                {
                    Console.WriteLine("#############################################################################");
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