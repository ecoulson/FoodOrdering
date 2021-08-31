using System;
using FileDatabase.API;

namespace FileDatabase.Collection
{
    public interface ICollectionReader<T> where T: IModel
    {
        ICollection<T> Read();
    }
}
