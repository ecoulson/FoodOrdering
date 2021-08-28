using System;
namespace FileDatabase.Document
{
    public class DocumentId: IDocumentId
    {
        public DocumentId(string id)
        {
        }

        public string Value => throw new NotImplementedException();
    }
}
