using System;
using System.Collections.Generic;
using Common;
using FileDatabase.Collection;
using FileDatabase.Document;
using FileDatabase.File;
using FileDatabase.JSON;

namespace FileDatabase.API
{
    public class FileDatabaseBuilder<T>: IFileDatabaseBuilder<T> where T: IModel
    {
        private IDirectory databaseDirectory;
        private ICollectionName collectionName;
        private IIOScheduler ioScheduler;

        public FileDatabaseBuilder()
        {
            ioScheduler = new IOScheduler();
            ioScheduler.StartSchedulingOperations();
        }

        public IFileDatabase<T> Build()
        {
            Assert.NotNull(databaseDirectory, "A database directory is needed to build the database");
            Assert.NotNull(collectionName, "A collection name is needed to build the database");

            var databaseAPI = new FileDatabase<T>(new DocumentManager<T>(
                collectionName,
                new CollectionReader<T>(collectionName, new JsonReader<Dictionary<string, T>>(new JsonTransformer(), new FileReader(ioScheduler)), databaseDirectory),
                new CollectionWriter<T>(collectionName, new JsonWriter<Dictionary<string, T>>(new JsonTransformer(), new FileWriter(ioScheduler)), databaseDirectory)
            ));
            Reset();
            return databaseAPI;
        }

        public void Reset()
        {
            databaseDirectory = null;
            collectionName = null;
        }

        public IFileDatabaseBuilder<T> WithDatabasePath(string databaseDirectory)
        {
            this.databaseDirectory = new Directory(databaseDirectory);
            return this;
        }

        public IFileDatabaseBuilder<T> WithName(string collectionName)
        {
            this.collectionName = new CollectionName(collectionName);
            return this;
        }
    }
}
