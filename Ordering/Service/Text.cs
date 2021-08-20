using System;
namespace Ordering.Service
{
    internal class Text: IText
    {
        public string Content { get; }

        public Text(string content)
        {
            Content = content;
        }

        public bool IsEmpty()
        {
            return Content.Trim().Length == 0;
        }
    }
}
