using System;
using System.Collections.Generic;
using Common;
using FileDatabase.API;
using FileDatabase.Document;
using FileDatabase.File;
using FileDatabase.JSON;

namespace FileDatabase.Collection
{

    internal class CollectionReader<T>: ICollectionReader<T> where T: IModel
    {
        private readonly ICollectionName collectionName;
        private readonly IJsonReader<Dictionary<string, T>> jsonReader;
        private readonly IDirectory databaseDirectory;

        public CollectionReader(ICollectionName collectionName, IJsonReader<Dictionary<string, T>> jsonReader, IDirectory databaseDirectory)
        {
            Assert.NotNull(collectionName, "[CollectionReader] Collection name can not be null");
            this.collectionName = collectionName;
            Assert.NotNull(jsonReader, "[CollectionReader] Json reader can not be null");
            this.jsonReader = jsonReader;
            Assert.NotNull(databaseDirectory, "[CollectionReader] Database directory can not be null");
            this.databaseDirectory = databaseDirectory;
        }

        public ICollection<T> Read()
        {
            Dictionary<string, T> collectionJSON = ReadCollectionJSON();
            return CreateCollectionFromJSON(collectionJSON);
        }

        private Dictionary<string, T> ReadCollectionJSON()
        {
            IFile file = GetCollectionFileWithDatabasePath();
            Assert.True(file.HasPath(), "[CollectionReader::ReadCollectionJSON] collection file needs to have a path");
            return jsonReader.Read(GetCollectionFileWithDatabasePath());
        }

        private IFile GetCollectionFileWithDatabasePath()
        {
            IFile file = collectionName.GetCollectionFile();
            file.Path = databaseDirectory.Path;
            return file;
        }

        private ICollection<T> CreateCollectionFromJSON(Dictionary<string, T> collectionJSON)
        {
            var result = new Collection<T>(collectionName);
            foreach (var jsonPair in collectionJSON)
            {
                result.AddDocument(CreateDocumentFromJSONPair(jsonPair));
            }
            return result;
        }

        private IDocument<T> CreateDocumentFromJSONPair(KeyValuePair<string, T> pair)
        {
            return new Document<T>(new DocumentId(pair.Key), pair.Value);
        }
    }
}
