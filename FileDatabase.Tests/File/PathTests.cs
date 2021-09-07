using System;
using FileDatabase.Exceptions;
using FileDatabase.File;
using Xunit;

namespace FileDatabase.Tests.File
{
    public class PathTests
    {
        private static readonly string IllegalFilePath = "foo/bar>/baz/";
        private static readonly string FilePathWithWhiteSpace = "foo\n/bar\t/b az/";
        private static readonly string FilePathPaddedWithWhiteSpace = " foo/bar/baz/ ";
        private static readonly string FilePathThatDoesNotEndWithSlash = "foo/bar";

        public PathTests()
        {
        }

        [Fact]
        public void WHEN_CreatingAFilePathFromNullString_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Path(null);
            });
        }

        [Fact]
        public void WHEN_CreatingAPathWithMoreThanMaxLength_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                new Path(GetMaxPathLengthString());
            });
        }

        private string GetMaxPathLengthString()
        {
            string path = "";
            for (int i = 0; i < FileConstants.MaxPathLength; i++)
            {
                path += "a";
            }
            path += "/";
            return path;
        }

        [Fact]
        public void WHEN_CreatingAFileWithPathThatHasIllegalCharacters_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                new Path(IllegalFilePath);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileWithPathThatHasWhiteSpace_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                new Path(FilePathWithWhiteSpace);
            });
        }

        [Fact]
        public void WHEN_CreatingAFileWithPathThatIsPaddedWithWhiteSpace_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                new Path(FilePathPaddedWithWhiteSpace);
            });
        }

        [Fact]
        public void WHEN_CreatingAFilePathThatEndsWithoutSlash_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                new Path(FilePathThatDoesNotEndWithSlash);
            });
        }
    }
}
