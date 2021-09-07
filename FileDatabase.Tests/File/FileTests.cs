using System;

namespace FileDatabase.Tests.File
{
    using FileDatabase.Exceptions;
    using Xunit;
    using FileDatabase.File;

    public class FileTests
    {
        private static readonly string DummyFileName = "file.txt";
        private static readonly string DummyFilePath = "/path/to/file/";
        private static readonly string DummyFileContents = "foo bar";

        private IFile file;

        public FileTests()
        {
            file = new File(DummyFileName);
        }

        [Fact]
        public void WHEN_SettingFileNameToNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                file.Name = null;
            });
        }

        [Fact]
        public void WHEN_CreatingAFileWithNullName_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new File(null);
            });
        }

        [Fact]
        public void WHEN_GettingContentFromFileWithNoContent_SHOULD_ThrowException()
        {
            Assert.Throws<NoContentException>(() =>
            {
                var content = file.Content;
            });
        }

        [Fact]
        public void WHEN_GettingPathFromFileWithNoPath_SHOULD_ThrowException()
        {
            Assert.Throws<NoPathException>(() =>
            {
                var path = file.Path;
            });
        }

        [Fact]
        public void WHEN_CreatingFile_SHOULD_HaveDummyName()
        {
            Assert.Equal(DummyFileName, file.Name);
        }

        [Fact]
        public void WHEN_SettingFileContentToNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                file.Content = null;
            });
        }

        [Fact]
        public void WHEN_SettingFilePathToNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                file.Path = null;
            });
        }

        [Fact]
        public void WHEN_SettingFileName_SHOULD_SetName()
        {
            file.Name = "Name";

            Assert.Equal("Name", file.Name);
        }

        [Fact]
        public void WHEN_SettingFilePath_SHOULD_SetPath()
        {
            file.Path = DummyFilePath;

            Assert.Equal(DummyFilePath, file.Path);
        }

        [Fact]
        public void WHEN_SettingFileContent_SHOULD_SetContent()
        {
            file.Content = DummyFileContents;

            Assert.Equal(DummyFileContents, file.Content);
        }

        [Fact]
        public void WHEN_CheckingIfFileHasContents_SHOULD_BeFalse()
        {
            Assert.False(file.HasContent());
        }

        [Fact]
        public void WHEN_CheckingIfFileHasPath_SHOULD_BeFalse()
        {
            Assert.False(file.HasPath());
        }
    }
}
