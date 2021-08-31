using System;
using FileDatabase.API;
using FileDatabase.Document;
using Moq;
using Xunit;

namespace FileDatabase.Tests.Document
{

    public class DocumentTests
    {
        private readonly Mock<IModel> mockModel;
        private readonly Document<IModel> document;

        public DocumentTests()
        {
            mockModel = new Mock<IModel>();
            document = new Document<IModel>(mockModel.Object);
        }

        [Fact]
        public void WHEN_CreatingADocumentWithNullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Document<IModel>(null);
            });
        }

        [Fact]
        public void WHEN_SettingADocumentWithNullModel_SHOULD_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                document.Model = null;
            });
        }

        [Fact]
        public void WHEN_CreatingADocument_SHOULD_HaveIdAndModel()
        {
            Assert.NotNull(document.Id);
            Assert.NotNull(document.Model);
        }

        [Fact]
        public void WHEN_SettingADocument_SHOULD_UpdateDocument()
        {
            document.Model = new Mock<IModel>().Object;

            Assert.NotNull(document);
        }
    }
}
