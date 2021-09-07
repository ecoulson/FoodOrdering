using System;
using Common.Id;

namespace FileDatabase.Document
{
    public class DocumentId: Id, IDocumentId
    {
        public DocumentId(): base(Guid.NewGuid().ToString())
        {

        }

        public DocumentId(string id): base(id)
        {
            Guid.Parse(id);
        }
    }
}
