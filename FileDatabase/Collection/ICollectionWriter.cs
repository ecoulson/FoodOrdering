using System;
using FileDatabase.API;

namespace FileDatabase.Collection
{
    public interface ICollectionWriter<T> where T: IModel
    {
        void Write(ICollection<T> collection);
    }
}
