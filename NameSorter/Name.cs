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
        private string[] _forenames;
        private string _surname;

        public Name(string rawNameText, NameFormats? format)
        {
            if (format == null) format = NameFormats.InOrder;
            
            BuildNameFromRawString(rawNameText, (NameFormats)format);
        }

        public Name(string[] forenames, string surname)
        {
            Init(forenames, surname);
        }

        private void Init(string[] forenames, string surname)
        {
            _forenames = forenames;
            _surname = surname;
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

            Init(forenames, surname);
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

            Init(forenames, surname);
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

            Init(forenames, surname);
        }

        public int CompareTo(Name name)
        {
            var ret = string.Compare(_surname, name._surname, StringComparison.Ordinal);

            if (ret != 0) return ret;

            var forenames = string.Join(" ", _forenames);
            var nameForenames = string.Join(" ", name._forenames);
            ret = string.Compare(forenames, nameForenames, StringComparison.Ordinal);

            return ret;
        }

        public override string ToString()
        {
            var forenames = string.Join(" ", _forenames);
            return forenames + " " + _surname;
            //return "fns:" + forenames + " sn:" + _surname;
        }
    }
}