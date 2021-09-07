using System;
using FileDatabase.Collection;
using FileDatabase.Document;

namespace FileDatabase.Exceptions
{
    public class DocumentNotFoundException: Exception
    {
        public DocumentNotFoundException(ICollectionName collectionName, IDocumentId id):
            base($"Could not find document with id {id.Value} in collection {collectionName.Value}")
        {
        }
    }
}
