using System;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.File
{
    public class FileWriter: IFileWriter
    {
        private readonly IIOScheduler ioScheduler;

        public FileWriter(IIOScheduler ioScheduler)
        {
            Assert.NotNull(ioScheduler, "[FileWriter] ioScheduler can not be null");
            this.ioScheduler = ioScheduler;
        }

        public IFile Write(IFile file)
        {
            Assert.NotNull(file, "[FileWriter::Write] file can not be null");
            if (!file.HasPath())
            {
                throw new NoPathException(file);
            }
            if (!file.HasContent())
            {
                throw new NoContentException(file);
            }
            return ioScheduler.RequestOperation(CreateWriteOperation(file)).Result;
        }

        private Func<IFile> CreateWriteOperation(IFile file)
        {
            return () => WriteFile(file);
        }

        private IFile WriteFile(IFile file)
        {
            System.IO.File.WriteAllText(file.Path, file.Content);
            return file;
        }
    }
}
