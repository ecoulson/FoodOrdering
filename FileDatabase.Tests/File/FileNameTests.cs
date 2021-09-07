using System;
using FileDatabase.Exceptions;
using FileDatabase.File;
using Xunit;

namespace FileDatabase.Tests.File
{
    public class FileNameTests
    {
        private static readonly string LongFileName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        private static readonly string IllegalFileName = "foo/>|:&.txt";
        private static readonly string FileNameWithWhiteSpace = "f o\t\no.txt";
        private static readonly string FileNameStartingAndEndingWithWhiteSpace = " foo ";

        [Fact]
        public void WHEN_CreatingAFileWithNullName_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FileName(null);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileWithANameWithLengthGreaterThan255_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFileNameException>(() =>
            {
                new FileName(LongFileName);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileWithANamePaddedWithWhitespace_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFileNameException>(() =>
            {
                new FileName(FileNameStartingAndEndingWithWhiteSpace);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileNameWithIllegalCharacters_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFileNameException>(() =>
            {
                new FileName(IllegalFileName);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileNameWithWhiteSpace_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFileNameException>(() =>
            {
                new FileName(FileNameWithWhiteSpace);
            });
        }
    }
}
