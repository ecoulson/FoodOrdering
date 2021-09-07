using System;
using Common;
using FileDatabase.API;

namespace FileDatabase.Document
{
    internal class Document<T> : IDocument<T> where T : IModel
    {
        private T model;

        public IDocumentId Id { get; }

        public T Model
        {
            get => model;
            set {
                Assert.NotNull(value, "[Document::Model{set}] value can not be null");
                model = value;
            }
        }

        public Document(T model): this(new DocumentId(), model)
        {
        }

        public Document(IDocumentId id, T model)
        {
            Assert.NotNull(model, "[Document] model can not be null");
            this.model = model;
            Assert.NotNull(id, "[Document] id can not be null");
            Id = id;
        }
    }
}
