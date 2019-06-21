using System;
using System.ComponentModel.DataAnnotations;
using NameSorter;
using Xunit;

namespace NameSorterUnitTests
{
    public class NameUnitTests
    {
        [Fact]
        public void TestInOrderFormatConstruction()
        {
            const string inputStr = "James Henry Britton";
            var expectedObject = new Name("James Henry", "Britton");

            var testResultObj = new Name(inputStr, NameFormats.InOrder);
            
            Assert.Matches(expectedObject.ToString(), testResultObj.ToString());
        }

        [Fact]
        public void TestBackwardFormatConstruction()
        {
            const string inputStr = "Britton Henry James";
            var expectedObject = new Name("James Henry", "Britton");
            
            var testResultObj = new Name(inputStr, NameFormats.Backwards);
            
            Assert.Matches(expectedObject.ToString(), testResultObj.ToString());
        }
        
        [Fact]
        public void TestSurnameFirstFormatConstruction()
        {
            const string inputStr = "Britton James Henry";
            var expectedObject = new Name("James Henry", "Britton");
            
            var testResultObj = new Name(inputStr, NameFormats.SurnameFirst);
            
            Assert.Matches(expectedObject.ToString(), testResultObj.ToString());
        }
        
        [Fact]
        public void TestNameCompareTo()
        {
            var name1 = new Name("Alfred Henry", "Britton");
            var name2 = new Name("Alfred James", "Britton");
            var name3 = new Name("Alice Laura", "Britton");
            var name4 = new Name("James Henry", "Britton");
            var name5 = new Name("James Henry", "Britton");

            Assert.True(name1.CompareTo(name2) < 0);
            Assert.True(name2.CompareTo(name3) < 0);
            Assert.True(name3.CompareTo(name4) < 0);
            
            Assert.True(name4.CompareTo(name5) == 0);
            
            Assert.True(name4.CompareTo(name3) > 0);
            Assert.True(name3.CompareTo(name2) > 0);
            Assert.True(name2.CompareTo(name1) > 0);
        }

        [Fact]
        public void TestNameToString()
        {
            var inputName = new Name("Test", "Name");
            var expectedOutput = "Test Name";

            var testRes = inputName.ToString();
            
            Assert.Equal(expectedOutput, testRes);
        }
    }
}