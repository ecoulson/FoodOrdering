using System;
using FileDatabase.API;
using FileDatabase.Exceptions;
using Xunit;

namespace FileDatabase.Tests.API
{
    public class FileDatabaseBuilderTests
    {
        private static string DummyInvalidPath = "foo";
        private static string DummyInvalidName = "foo-bar";
        private static string DummyValidPath = "foo/";
        private static string DummyValidName = "FooBar";

        private readonly IFileDatabaseBuilder<IModel> fileDatabaseBuilder;

        public FileDatabaseBuilderTests()
        {
            fileDatabaseBuilder = new FileDatabaseBuilder<IModel>();
        }

        [Fact]
        public void WHEN_CreatingADatabaseWithAnInvalidPath_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalFilePathException>(() =>
            {
                fileDatabaseBuilder.WithDatabasePath(DummyInvalidPath);
            });
        }

        [Fact]
        public void WHEN_CreatingADatabaseWithIllegalCollectionName_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalCollectionNameException>(() =>
            {
                fileDatabaseBuilder.WithName(DummyInvalidName);
            });
        }

        [Fact]
        public void WHEN_CreatingADatabaseWithNullCollectionName_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabaseBuilder
                    .WithDatabasePath(DummyValidPath)
                    .Build();
            });
        }

        [Fact]
        public void WHEN_CreatingADatabaseWithNullPath_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                fileDatabaseBuilder
                    .WithName(DummyValidName)
                    .Build();
            });
        }

        [Fact]
        public void WHEN_CreatingADatabase_SHOULD_CreateADatabase()
        {
            var result = fileDatabaseBuilder
                .WithDatabasePath(DummyValidPath)
                .WithName(DummyValidName)
                .Build();

            Assert.NotNull(result);
        }
    }
}
