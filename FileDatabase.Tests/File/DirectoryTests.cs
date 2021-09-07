using System;
using FileDatabase.File;
using Xunit;

namespace FileDatabase.Tests.File
{
    public class DirectoryTests
    {
        private readonly static string DummyPath = "foo/bar/";
        private readonly static string DummySetPath = "foo/bar/baz/";

        public DirectoryTests()
        {
        }

        [Fact]
        public void WHEN_CreatingADirectoryWithNullPath_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Directory(null);
            });
        }

        [Fact]
        public void WHEN_SettingADirectoryPathToNull_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var directory = new Directory(DummyPath);

                directory.Path = null;
            });
        }

        [Fact]
        public void WHEN_SettingADirectoryPath_SHOULD_UpdateThePath()
        {
            var directory = new Directory(DummyPath);

            directory.Path = DummySetPath;

            Assert.Equal(DummySetPath, directory.Path);
        }
    }
}
