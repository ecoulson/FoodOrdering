using System;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.File
{
    public class FileReader: IFileReader
    {
        private readonly IIOScheduler ioScheduler;

        public FileReader(IIOScheduler ioScheduler)
        {
            Assert.NotNull(ioScheduler, "[FileReader] file synchronizer can not be null");
            this.ioScheduler = ioScheduler;
        }

        public IFile Read(IFile file)
        {
            Assert.NotNull(file, "[FileReader::Read] file can not be null");
            if (!file.HasPath())
            {
                throw new NoPathException(file);
            }
            return ioScheduler.RequestOperation(CreateReadOperationFunction(file)).Result;
        }

        private Func<IFile> CreateReadOperationFunction(IFile file)
        {
            return () => ReadFileContents(file);
        }

        private IFile ReadFileContents(IFile file)
        {
            file.Content = System.IO.File.ReadAllText(file.Path);
            return file;
        }
    }
}
