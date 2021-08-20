using System;
namespace Ordering.Service
{
    public interface IText
    {
        string Content { get; }
        bool IsEmpty();
    }
}
