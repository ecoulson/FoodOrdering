using System;
namespace FileDatabase.API
{
    public interface IFileDatabaseBuilder<T> where T: IModel
    {
        IFileDatabaseBuilder<T> WithName(string collectionName);
        IFileDatabaseBuilder<T> WithDatabasePath(string path);
        IFileDatabase<T> Build();
        void Reset();
    }
}
