using System;
using Common;
using FileDatabase.Document;

namespace FileDatabase.API
{
    internal class FileDatabase<T>: IFileDatabase<T>
    {
        private readonly IDocumentManager<T> documentManager;

        public FileDatabase(IDocumentManager<T> documentManager)
        {
            Assert.NotNull(documentManager, "[FileDatabase] document manager can not be null");
            this.documentManager = documentManager;
        }

        public IDocument<T> CreateDocument(T model)
        {
            Assert.NotNull(model, "[FileDatabase::CreateDocument] model can not be null");
            return documentManager.Create(model);
        }

        public void DeleteDocument(IDocumentId id)
        {
            Assert.NotNull(id, "[FileDatabase::DeleteDocument] id can not be null");
            documentManager.Delete(id);
        }

        public IDocument<T> ReadDocument(IDocumentId id)
        {
            Assert.NotNull(id, "[FileDatabase::ReadDocument] id can not be null");
            return documentManager.Read(id);
        }

        public IDocument<T> UpdateDocument(IDocumentId id, T model)
        {
            Assert.NotNull(id, "[FileDatabase::UpdateDocument] id can not be null");
            Assert.NotNull(model, "[FileDatabase::UpdateDocument] model can not be null");
            return documentManager.Update(id, model);
        }
    }
}
