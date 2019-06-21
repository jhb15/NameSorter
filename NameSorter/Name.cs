using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NameSorter
{
    /**
     * Class used to represent a name in the NameSorter program.
     */
    public class Name
    {
        public string Forenames;
        public string Surname;

        /**
         * Constructor that takes a raw name string and construct the name using this, this also take in an parameter for
         * Name Format, if this is null it defaults to in order.
         */
        public Name(string rawNameText, NameFormats? format)
        {
            if (format == null) format = NameFormats.InOrder;
            
            BuildNameFromRawString(rawNameText, (NameFormats)format);
        }

        /**
         * Simple constructor that takes in the values for both forenames and surnames directly instead of having to
         * extract these values from an input string.
         */
        public Name(string forenames, string surname)
        {
            Init(forenames, surname);
        }

        private void Init(string forenames, string surname)
        {
            Forenames = forenames;
            Surname = surname;
        }

        private void BuildNameFromRawString(string rawString, NameFormats format)
        {
            var nameComponents = Regex.Split(rawString, @"\s+");
            var cleanComponents = SanitiseInputName(nameComponents);
            
            switch (format)
            {
                case NameFormats.InOrder:
                    BuildNameFromInOrderString(cleanComponents);
                    break;
                case NameFormats.Backwards:
                    BuildNameFromBackwardsString(cleanComponents);
                    break;
                case NameFormats.SurnameFirst:
                    BuildNameFromSurnameFirstString(cleanComponents);
                    break;
                default:
                    Console.WriteLine("Error name format not recognised or implemented! Format used: " + format);
                    break;
            }
        }

        /**
         * This function is used to remove any empty fields from the name component string array so that the name is
         * interpreted properly.
         */
        private string[] SanitiseInputName(string[] nameComponents)
        {
            List<string> componentList = new List<string>(nameComponents);
            
            componentList.RemoveAll(string.IsNullOrEmpty);
            componentList.RemoveAll(string.IsNullOrWhiteSpace);
            
            return componentList.ToArray();
        }

        private void BuildNameFromInOrderString(string[] nameComponents)
        {
            var forenames = new string[nameComponents.Length - 1];
            var surname = nameComponents.Last();

            for (var i = 0; i < (nameComponents.Length - 1); i++)
            {
                forenames[i] = nameComponents[i];
            }

            var foreStr = string.Join(" ", forenames);
            Init(foreStr, surname);
        }

        private void BuildNameFromBackwardsString(string[] nameComponents)
        {
            var forenames = new string[nameComponents.Length - 1];
            var surname = nameComponents.First();

            var j = 0;
            for (var i = (nameComponents.Length - 1); i > 0; i--)
            {
                forenames[j] = nameComponents[i];
                j++;
            }

            var foreStr = string.Join(" ", forenames);
            Init(foreStr, surname);
        }

        private void BuildNameFromSurnameFirstString(string[] nameComponents)
        {
            var forenames = new string[nameComponents.Length - 1];
            var surname = nameComponents.First();

            var j = 0;
            for (var i = 1; i < nameComponents.Length; i++)
            {
                forenames[j] = nameComponents[i];
                j++;
            }

            var foreStr = string.Join(" ", forenames);
            Init(foreStr, surname);
        }

        public int CompareTo(Name name)
        {
            var ret = string.Compare(Surname, name.Surname, StringComparison.Ordinal);

            if (ret != 0) return ret;

            ret = string.Compare(Forenames, name.Forenames, StringComparison.Ordinal);

            return ret;
        }

        public override string ToString()
        {
            return Forenames + " " + Surname;
            //return "fns:" + Forenames + " sn:" + Surname;
        }
    }
}