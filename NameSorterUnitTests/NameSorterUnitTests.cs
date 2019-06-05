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
        private readonly string testFilePath = "testFile.txt";
        readonly string[] inputFile = {"Cfirst Cmiddle Csurname",
                                    "Dfirst Zmiddle Dsurname   ",
                                    "Dfirst Bmiddle Dsurname",
                                    "Bfirst Bmiddle Bsurname",
                                    "Afirst Amiddle Asurname"};

        readonly string[] outputFile = {"Afirst Amiddle Asurname",
                                    "Bfirst Bmiddle Bsurname",
                                    "Cfirst Cmiddle Csurname",
                                    "Dfirst Bmiddle Dsurname",
                                    "Dfirst Zmiddle Dsurname"};

        [Fact]
        public void TestSort()
        {
            string testOutPath = "sorted" + testFilePath;
            
            Mock<IFileWrapper> mockFileWrapper = new Mock<IFileWrapper>();
            mockFileWrapper.Setup<string[]>(m => m.ReadAllLines(It.Is<string>(s=> String.Compare(s, testFilePath, StringComparison.Ordinal) == 0))).Returns(inputFile);
            Mock<StreamWriter> streamWriter = new Mock<StreamWriter>(testOutPath);
            mockFileWrapper.Setup<StreamWriter>(m =>
                    m.GetStreamWriter(It.Is<string>(s =>
                        string.Compare(s, testOutPath, StringComparison.Ordinal) == 0)))
                .Returns(streamWriter.Object);
            
            global::NameSorter.NameSorter nameSorter = new global::NameSorter.NameSorter(mockFileWrapper.Object);
            nameSorter.Sort(testFilePath, NameFormats.InOrder);

            foreach (var line in outputFile)
            {
                streamWriter.Verify(a => a.WriteLine(line), Times.Exactly(1));
            }
        }
    }
}