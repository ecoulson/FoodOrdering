using System;
namespace FileDatabase.Document
{
    public interface IDocument<T>
    {
        IDocumentId Id { get; }
        T Model { get; set;  }
    }
}
