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
            var name = new CollectionName(DummyCollectionName);

            Assert.Equal(DummyCollectionName, name.Value);
        }
    }
}
