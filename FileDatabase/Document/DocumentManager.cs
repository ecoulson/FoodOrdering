using System;
using Common;
using FileDatabase.API;
using FileDatabase.Collection;
using FileDatabase.Exceptions;

namespace FileDatabase.Document
{
    internal class DocumentManager<T>: IDocumentManager<T> where T: IModel
    {
        private readonly ICollectionName name;
        private readonly ICollectionReader<T> collectionReader;
        private readonly ICollectionWriter<T> collectionWriter;

        public DocumentManager(ICollectionName name, ICollectionReader<T> collectionReader, ICollectionWriter<T> collectionWriter)
        {
            Assert.NotNull(name, "[DocumentManager] database name can not be null");
            this.name = name;
            Assert.NotNull(collectionReader, "[DocumentManager] collection reader can not be null");
            this.collectionReader = collectionReader;
            Assert.NotNull(collectionWriter, "[DocumentManager] collection writer can not be null");
            this.collectionWriter = collectionWriter;
        }

        public IDocument<T> Create(T model)
        {
            Assert.NotNull(model, "[DocumentManager::Create] model can not be null");

            var collection = collectionReader.Read();
            var document = new Document<T>(model);
            collection.AddDocument(document);
            collectionWriter.Write(collection);
            return document;
        }

        public void Delete(IDocumentId id)
        {
            Assert.NotNull(id, "[DocumentManager::Delete] id can not be null");

            var collection = collectionReader.Read();
            AssertIdExistsInCollection(collection, id);
            collection.RemoveDocumentById(id);
            collectionWriter.Write(collection);
        }

        private void AssertIdExistsInCollection(ICollection<T> collection, IDocumentId id)
        {
            if (!collection.HasDocument(id))
            {
                throw new DocumentNotFoundException(name, id);
            }
        }

        public IDocument<T> Read(IDocumentId id)
        {
            Assert.NotNull(id, "[DocumentManager::Read] id can not be null");

            var collection = collectionReader.Read();
            AssertIdExistsInCollection(collection, id);
            return collection.GetDocumentById(id);
        }

        public IDocument<T> Update(IDocumentId id, T model)
        {
            Assert.NotNull(id, "[DocumentManager::Update] id can not be null");
            Assert.NotNull(model, "[DocumentManager::Update] model can not be null");

            var collection = collectionReader.Read();
            AssertIdExistsInCollection(collection, id);
            var document = UpdateCollection(collection, id, model);
            collectionWriter.Write(collection);
            return document;
        }

        private IDocument<T> UpdateCollection(ICollection<T> collection, IDocumentId id, T model)
        {
            var document = collection.GetDocumentById(id);
            document.Model = model;
            collection.UpdateDocument(document);
            return document;
        }
    }
}
