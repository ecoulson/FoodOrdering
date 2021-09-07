using System;
using Common;

namespace FileDatabase.File
{
    public class Directory: IDirectory
    {
        private IPath path;

        public string Path
        {
            get { return path.Value; }
            set
            {
                Assert.NotNull(value, "[Directory]{set} value can not be null");
                path = new Path(value);
            }
        }

        public Directory(string path)
        {
            Assert.NotNull(path, "[Path] path should not be null");
            this.path = new Path(path);
        }
    }
}
