using System;
using Common;
using FileDatabase.API;
using FileDatabase.File;

namespace FileDatabase.JSON
{
    public class JsonReader<T>: IJsonReader<T>
    {
        private readonly IJsonTransformer jsonTransformer;
        private readonly IFileReader fileReader;

        public JsonReader(IJsonTransformer jsonTransformer, IFileReader fileReader)
        {
            Assert.NotNull(jsonTransformer, "[JsonReader] json transformer can not be null");
            this.jsonTransformer = jsonTransformer;
            Assert.NotNull(fileReader, "[JsonReader] file reader can not be null");
            this.fileReader = fileReader;
        }

        public T Read(IFile file)
        {
            Assert.NotNull(file, "[JsonReader::Read] file can not be null");
            IFile fileWithContents = fileReader.Read(file);
            Assert.True(fileWithContents.HasContent(), "[JsonReader::Read] file that has been read must have some content");
            return jsonTransformer.Deserialize<T>(fileWithContents.Content);
        }
    }
}
