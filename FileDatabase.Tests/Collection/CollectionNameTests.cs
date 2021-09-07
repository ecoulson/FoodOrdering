using System;
namespace FileDatabase.Tests.Collection
{
    using FileDatabase.Collection;
    using FileDatabase.Exceptions;
    using Xunit;

    public class CollectionNameTests
    {
        private static readonly string DummyIllegalCollectionName = "Test.Collection";
        private static readonly string DummyCollectionName = "TestCollection";
        private static readonly string DummyCollectionFileName = "TestCollection.json";

        private ICollectionName name;

        public CollectionNameTests()
        {
            name = new CollectionName(DummyCollectionName);
        }

        [Fact]
        public void WHEN_CreatingACollectionNameWithANonAlphabeticalCharacter_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalCollectionNameException>(() =>
            {
                new CollectionName(DummyIllegalCollectionName);
            });
        }


        [Fact]
        public void WHEN_CreatingACollectionNameWithAnEmptyName_SHOULD_ThrowException()
        {
            Assert.Throws<IllegalCollectionNameException>(() =>
            {
                new CollectionName("");
            });
        }

        [Fact]
        public void WHEN_CreatingACollectionNameWithANullString_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CollectionName(null);
            });
        }

        [Fact]
        public void WHEN_CreatingACollectionName_SHOULD_CreateCollectioName()
        {
            Assert.Equal(DummyCollectionName, name.Value);
        }

        [Fact]
        public void WHEN_GettingCollectionFile_SHOULD_ReturnFile()
        {
            var file = name.GetCollectionFile();

            Assert.Equal(DummyCollectionFileName, file.Name);
        }
    }
}
