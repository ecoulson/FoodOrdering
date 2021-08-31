using System;
using FileDatabase.API;

namespace FileDatabase.Document
{
    public interface IDocumentManager<T> where T: IModel
    {
        IDocument<T> Create(T model);
        IDocument<T> Read(IDocumentId id);
        IDocument<T> Update(IDocumentId id, T model);
        void Delete(IDocumentId id);
    }
}
