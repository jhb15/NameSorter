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
            switch (format)
            {
                case NameFormats.InOrder:
                    BuildNameFromInOrderString(rawString);
                    break;
                case NameFormats.Backwards:
                    BuildNameFromBackwardsString(rawString);
                    break;
                case NameFormats.SurnameFirst:
                    BuildNameFromSurnameFirstString(rawString);
                    break;
                default:
                    Console.WriteLine("Error name format not recognised! Format used: " + format);
                    break;
            }
        }

        private string[] SanitiseInputName(string[] nameComponents)
        {
            List<string> componentList = new List<string>(nameComponents);
            var i = 0;
            List<int> componentsToRemove = new List<int>();
            foreach (var component in componentList)
            {
                var space = string.Compare(component, " ", StringComparison.Ordinal);
                var newLine = string.Compare(component, "\n", StringComparison.Ordinal);
                var nothing = string.Compare(component, "", StringComparison.Ordinal);
                if ((nothing == 0) || (newLine == 0) || (space == 0))
                {
                    //componentList.Remove(component); //TODO For some reason this didn't work
                    componentsToRemove.Add(i);
                }
                i++;
            }
            foreach (var index in componentsToRemove)
            {
                componentList.RemoveAt(index);
            }
            return componentList.ToArray();
        }

        private void BuildNameFromInOrderString(string rawString)
        {
            //var nameComponents = rawString.Split(null);    
            var nameComponents = Regex.Split(rawString, @"\s+");
            var cleanComponents = SanitiseInputName(nameComponents);
            var forenames = new string[cleanComponents.Length - 1];
            var surname = cleanComponents.Last();

            for (var i = 0; i < (cleanComponents.Length - 1); i++)
            {
                forenames[i] = cleanComponents[i];
            }

            var foreStr = string.Join(" ", forenames);
            Init(foreStr, surname);
        }

        private void BuildNameFromBackwardsString(string rawString)
        {
            var nameComponents = Regex.Split(rawString, @"\s+");
            var cleanComponents = SanitiseInputName(nameComponents);
            var forenames = new string[cleanComponents.Length - 1];
            var surname = cleanComponents.First();

            var j = 0;
            for (var i = (cleanComponents.Length - 1); i > 0; i--)
            {
                forenames[j] = cleanComponents[i];
                j++;
            }

            var foreStr = string.Join(" ", forenames);
            Init(foreStr, surname);
        }

        private void BuildNameFromSurnameFirstString(string rawString)
        {
            var nameComponents = Regex.Split(rawString, @"\s+");
            var cleanComponents = SanitiseInputName(nameComponents);
            var forenames = new string[cleanComponents.Length - 1];
            var surname = cleanComponents.First();

            var j = 0;
            for (var i = 1; i < cleanComponents.Length; i++)
            {
                forenames[j] = cleanComponents[i];
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
            var forenames = string.Join(" ", Forenames);
            return forenames + " " + Surname;
            //return "fns:" + forenames + " sn:" + _surname;
        }
    }
}