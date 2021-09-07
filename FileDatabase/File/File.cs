using System;
using Common;
using FileDatabase.Exceptions;

namespace FileDatabase.File
{
    public class File: IFile
    {
        private IFileName name;
        private IPath path;
        private string content;

        public File(string name)
        {
            Assert.NotNull(name, "[File] name can not be null");
            this.name = new FileName(name);
        }

        public string Name
        {
            get { return name.Value; }
            set
            {
                Assert.NotNull(value, "[File::Name]{set} value can not be null");
                name = new FileName(value);
            }
        }

        public string Path
        {
            get
            {
                if (!HasPath())
                {
                    throw new NoPathException(this);
                }
                return path.Value;
            }
            set
            {
                Assert.NotNull(value, "[File::Path]{set} value can not be null");
                path = new Path(value);
            }
        }

        public string Content
        {
            get
            {
                if (!HasContent())
                {
                    throw new NoContentException(this);
                }
                return content;
            }
            set
            {
                Assert.NotNull(value, "[File::Content]{set} value can not be null");
                content = value;
            }
        }

        public bool HasContent()
        {
            return content != null;
        }

        public bool HasPath()
        {
            return path != null;
        }
    }
}
