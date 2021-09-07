using System;
namespace FileDatabase.File
{
    public interface IFile
    {
        string Path { get; set; }
        string Name { get; set; }
        string Content { get; set; }
        bool HasContent();
        bool HasPath();
    }
}
