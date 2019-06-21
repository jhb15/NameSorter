using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NameSorter;
using NameSorter.Interfaces;
using Xunit;
using NameSorter = NameSorter.NameSorter;

namespace NameSorterUnitTests
{
    public class NameSorterUnitTests
    {
        readonly string TestFilePath = "testFile.txt";

        readonly string[] _inputFileInOrder = {"Cfirst Cmiddle Csurname",
            "Dfirst Zmiddle Dsurname   ",
            "Dfirst Bmiddle Dsurname",
            "Bfirst Bmiddle Bsurname",
            "Afirst Amiddle Asurname"};
        
        readonly string[] _inputFileSurnameFirst = {"Csurname Cfirst Cmiddle",
            "Dsurname Dfirst Zmiddle   ",
            "Dsurname Dfirst Bmiddle",
            "Bsurname Bfirst Bmiddle",
            "Asurname Afirst Amiddle"};
        
        readonly string[] _inputFileBackwards = {"Csurname Cmiddle Cfirst",
            "Dsurname Zmiddle Dfirst  ",
            "Dsurname Bmiddle Dfirst",
            "Bsurname Bmiddle Bfirst  ",
            "Asurname Amiddle Afirst"};


        private readonly string[] _outputFileAsc = 
        {
            "Afirst Amiddle Asurname",
            "Bfirst Bmiddle Bsurname",
            "Cfirst Cmiddle Csurname",
            "Dfirst Bmiddle Dsurname",
            "Dfirst Zmiddle Dsurname"
        };

        private readonly string[] _outputFileDec =
        {
            "Dfirst Zmiddle Dsurname",
            "Dfirst Bmiddle Dsurname",
            "Cfirst Cmiddle Csurname",
            "Bfirst Bmiddle Bsurname",
            "Afirst Amiddle Asurname"
        };

        private void TestSort(bool order, NameFormats format, string[] input, string[] output)
        {
            string testOutPath = "sorted" + TestFilePath;
            
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup<string[]>(m => m.ReadAllLines(It.Is<string>(s=> String.Compare(s, TestFilePath, StringComparison.Ordinal) == 0))).Returns(input);
            Mock<StreamWriter> streamWriter = new Mock<StreamWriter>(testOutPath);
            mockFileWrapper.Setup<StreamWriter>(m =>
                    m.GetStreamWriter(It.Is<string>(s =>
                        string.Compare(s, testOutPath, StringComparison.Ordinal) == 0)))
                .Returns(streamWriter.Object);
            
            global::NameSorter.NameSorter nameSorter = new global::NameSorter.NameSorter(mockFileWrapper.Object);
            nameSorter.Sort(TestFilePath, format, order);

            foreach (var line in output)
            {
                streamWriter.Verify(a => a.WriteLine(line), Times.Exactly(1));
            }
        }

        [Fact]
        public void TestSort_InOrderNames_Asc()
        {
            TestSort(true, NameFormats.InOrder, _inputFileInOrder, _outputFileAsc);
        }
        
        [Fact]
        public void TestSort_SurnameFirstNames_Asc()
        {
            TestSort(true, NameFormats.SurnameFirst, _inputFileSurnameFirst, _outputFileAsc);
        }
        
        [Fact]
        public void TestSort_BackwardsNames_Asc()
        {
            TestSort(true, NameFormats.Backwards, _inputFileBackwards, _outputFileAsc);
        }
        
        [Fact]
        public void TestSort_InOrderNames_Dec()
        {
            TestSort(false, NameFormats.InOrder, _inputFileInOrder, _outputFileDec);
        }
        
        [Fact]
        public void TestSort_SurnameFirstNames_Dec()
        {
            TestSort(false, NameFormats.SurnameFirst, _inputFileSurnameFirst, _outputFileDec);
        }
        
        [Fact]
        public void TestSort_BackwardsNames_Dec()
        {
            TestSort(false, NameFormats.Backwards, _inputFileBackwards, _outputFileDec);
        }
    }
}
