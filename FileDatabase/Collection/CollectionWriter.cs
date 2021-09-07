using System;
using System.Collections.Generic;
using Common;
using FileDatabase.API;
using FileDatabase.Document;
using FileDatabase.File;
using FileDatabase.JSON;

namespace FileDatabase.Collection
{
    public class CollectionWriter<T> : ICollectionWriter<T> where T : IModel
    {
        private readonly ICollectionName collectionName;
        private readonly IJsonWriter<Dictionary<string, T>> jsonWriter;
        private readonly IDirectory databaseDirectory;

        public CollectionWriter(ICollectionName collectionName, IJsonWriter<Dictionary<string, T>> jsonWriter, IDirectory databaseDirectory)
        {
            Assert.NotNull(collectionName, "[CollectionWriter] collection name can not be null");
            this.collectionName = collectionName;
            Assert.NotNull(jsonWriter, "[CollectionWriter] json writer can not be null");
            this.jsonWriter = jsonWriter;
            Assert.NotNull(databaseDirectory, "[CollectionWriter] database directory can not be null");
            this.databaseDirectory = databaseDirectory;
        }

        public void Write(ICollection<T> collection)
        {
            Assert.NotNull(collection, "[CollectionWriter::Write] collection can not be null");
            var file = GetCollectionFileWithDatabasePath();
            Assert.True(file.HasPath(), "[CollectionWriter::Write] file must have a path");
            jsonWriter.Write(file, SerializeCollectionToJson(collection));
        }

        private IFile GetCollectionFileWithDatabasePath()
        {
            IFile file = collectionName.GetCollectionFile();
            file.Path = databaseDirectory.Path;
            return file;
        }

        private Dictionary<string, T> SerializeCollectionToJson(ICollection<T> collection)
        {
            var json = new Dictionary<string, T>();
            foreach (var document in collection)
            {
                json.Add(document.Id.Value, document.Model);
            }
            return json;
        }
    }
}
