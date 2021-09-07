using System;
using FileDatabase.API;
using FileDatabase.Collection;
using FileDatabase.Document;

namespace FileDatabase.Exceptions
{
    public class DocumentExistsException: Exception
    {
        public DocumentExistsException(ICollectionName collectionName, IDocumentId id):
            base($"Document with id {id.Value} already exists in collection {collectionName.Value}")
        {
        }
    }
}
