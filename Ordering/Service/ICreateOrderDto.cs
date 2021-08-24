using System;
using System.Collections.Generic;

namespace Ordering.Service
{
    public interface ICreateOrderDto
    {
        string PhoneNumber { get; set; }
        DateTime RecievedDateTime { get; set; }
        string Content { get; set; }
    }
}
