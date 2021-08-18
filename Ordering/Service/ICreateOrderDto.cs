using System;
using System.Collections.Generic;

namespace Ordering
{
    public interface ICreateOrderDto
    {
        IPhoneNumber PhoneNumber { get; set; }
        DateTime RecievedDateTime { get; set; }
        IText Content { get; set; }
    }
}
