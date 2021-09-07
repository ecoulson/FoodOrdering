using System;
namespace FileDatabase.File
{
    public class FileConstants
    {
        public static readonly char[] ReservedCharacters = new char[]
        {
            ' ', ':', '|', '&', '>', '\t', '\n'
        };
        public static readonly int MaxFileNameLength = 255;
        public static readonly int MaxPathLength = 4096;
        public static readonly char PathSeperator = '/';
    }
}
