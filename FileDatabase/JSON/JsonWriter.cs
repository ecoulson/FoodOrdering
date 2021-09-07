using System;
using Common;
using FileDatabase.File;

namespace FileDatabase.JSON
{
    public class JsonWriter<T>: IJsonWriter<T>
    {
        private readonly IJsonTransformer transformer;
        private readonly IFileWriter fileWriter;

        public JsonWriter(IJsonTransformer transformer, IFileWriter fileWriter)
        {
            Assert.NotNull(transformer, "[JsonWriter] transformer can not be null");
            this.transformer = transformer;
            Assert.NotNull(fileWriter, "[JsonWriter] file writer can not be null");
            this.fileWriter = fileWriter;
        }

        public void Write(IFile file, T obj)
        {
            Assert.NotNull(file, "[JsonWriter::Write] file can not be null");
            Assert.NotNull(obj, "[JsonWriter::Write] objcan not be null");

            file.Content = transformer.Serialize<T>(obj);
            fileWriter.Write(file);
        }
    }
}
