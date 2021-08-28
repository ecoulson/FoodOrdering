using System;
using Common.Id;

namespace FileDatabase.Document
{
    public class DocumentId: Id, IDocumentId
    {
        public DocumentId(IId id): base(id)
        {
            Guid.Parse(id.Value);
        }
    }
}
