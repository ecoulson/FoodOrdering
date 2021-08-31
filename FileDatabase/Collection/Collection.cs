using System;
using System.Collections.Generic;
using Common;
using FileDatabase.API;
using FileDatabase.Document;
using FileDatabase.Exceptions;

namespace FileDatabase.Collection
{
    internal class Collection<T>: ICollection<T> where T: IModel
    {
        private readonly ICollectionName name;
        private readonly Dictionary<IDocumentId, IDocument<T>> documentTable;

        public Collection(ICollectionName name)
        {
            Assert.NotNull(name, "[Collection] name can not be null");
            this.name = name;
            documentTable = new Dictionary<IDocumentId, IDocument<T>>();
        }

        public void AddDocument(IDocument<T> document)
        {
            Assert.NotNull(document, "[Collection::AddDocument] document can not be null");
            AssertIdDoesNotInDocumentTable(document.Id);
            documentTable.Add(document.Id, document);
        }

        private void AssertIdDoesNotInDocumentTable(IDocumentId id)
        {
            if (HasDocument(id))
            {
                throw new DocumentExistsException(name, id);
            }
        }

        public IDocument<T> GetDocumentById(IDocumentId documentId)
        {
            Assert.NotNull(documentId, "[Collection::GetDocumentById] document id is null");
            AssertIdInDocumentTable(documentId);
            return documentTable[documentId];
        }

        private void AssertIdInDocumentTable(IDocumentId id)
        {
            if (!HasDocument(id))
            {
                throw new DocumentNotFoundException(name, id);
            }
        }

        public bool HasDocument(IDocumentId documentId)
        {
            Assert.NotNull(documentId, "[Collection::HasDocument] document id can not be null");
            return documentTable.ContainsKey(documentId);
        }

        public void RemoveDocumentById(IDocumentId documentId)
        {
            Assert.NotNull(documentId, "[Collection::RemoveDocumentById] document id can not be null");
            AssertIdInDocumentTable(documentId);
            documentTable.Remove(documentId);
        }

        public IDocument<T> UpdateDocument(IDocument<T> document)
        {
            Assert.NotNull(document, "[Collection::UpdateDocument] document can not be null");
            AssertIdInDocumentTable(document.Id);
            documentTable[document.Id] = document;
            return GetDocumentById(document.Id);
        }
    }
}
