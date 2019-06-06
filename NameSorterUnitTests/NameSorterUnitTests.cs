using System;
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
        

        readonly string[] _outputFile = {"Afirst Amiddle Asurname",
            "Bfirst Bmiddle Bsurname",
            "Cfirst Cmiddle Csurname",
            "Dfirst Bmiddle Dsurname",
            "Dfirst Zmiddle Dsurname"};
        
        [Fact]
        public void TestSort_InOrderNames()
        {
            string testOutPath = "sorted" + TestFilePath;
            
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup<string[]>(m => m.ReadAllLines(It.Is<string>(s=> String.Compare(s, TestFilePath, StringComparison.Ordinal) == 0))).Returns(_inputFileInOrder);
            Mock<StreamWriter> streamWriter = new Mock<StreamWriter>(testOutPath);
            mockFileWrapper.Setup<StreamWriter>(m =>
                    m.GetStreamWriter(It.Is<string>(s =>
                        string.Compare(s, testOutPath, StringComparison.Ordinal) == 0)))
                .Returns(streamWriter.Object);
            
            global::NameSorter.NameSorter nameSorter = new global::NameSorter.NameSorter(mockFileWrapper.Object);
            nameSorter.Sort(TestFilePath, NameFormats.InOrder);

            foreach (var line in _outputFile)
            {
                streamWriter.Verify(a => a.WriteLine(line), Times.Exactly(1));
            }
        }
        
        [Fact]
        public void TestSort_SurnameFirstNames()
        {
            string testOutPath = "sorted" + TestFilePath;
            
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup<string[]>(m => m.ReadAllLines(It.Is<string>(s=> String.Compare(s, TestFilePath, StringComparison.Ordinal) == 0))).Returns(_inputFileSurnameFirst);
            Mock<StreamWriter> streamWriter = new Mock<StreamWriter>(testOutPath);
            mockFileWrapper.Setup<StreamWriter>(m =>
                    m.GetStreamWriter(It.Is<string>(s =>
                        string.Compare(s, testOutPath, StringComparison.Ordinal) == 0)))
                .Returns(streamWriter.Object);
            
            global::NameSorter.NameSorter nameSorter = new global::NameSorter.NameSorter(mockFileWrapper.Object);
            nameSorter.Sort(TestFilePath, NameFormats.SurnameFirst);

            foreach (var line in _outputFile)
            {
                streamWriter.Verify(a => a.WriteLine(line), Times.Exactly(1));
            }
        }
        
        [Fact]
        public void TestSort_BackwardsNames()
        {
            string testOutPath = "sorted" + TestFilePath;
            
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup<string[]>(m => m.ReadAllLines(It.Is<string>(s=> String.Compare(s, TestFilePath, StringComparison.Ordinal) == 0))).Returns(_inputFileBackwards);
            Mock<StreamWriter> streamWriter = new Mock<StreamWriter>(testOutPath);
            mockFileWrapper.Setup<StreamWriter>(m =>
                    m.GetStreamWriter(It.Is<string>(s =>
                        string.Compare(s, testOutPath, StringComparison.Ordinal) == 0)))
                .Returns(streamWriter.Object);
            
            global::NameSorter.NameSorter nameSorter = new global::NameSorter.NameSorter(mockFileWrapper.Object);
            nameSorter.Sort(TestFilePath, NameFormats.Backwards);

            foreach (var line in _outputFile)
            {
                streamWriter.Verify(a => a.WriteLine(line), Times.Exactly(1));
            }
        }
    }
}
