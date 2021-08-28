using System;
using FileDatabase.Document;

namespace FileDatabase.Exceptions
{
    public class DocumentNotFoundException: Exception
    {
        public DocumentNotFoundException(IDocumentId id): base($"Could not find document with id ${id.Value}")
        {
        }
    }
}
