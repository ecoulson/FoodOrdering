using System;
using System.Text.RegularExpressions;

namespace Ordering.OrderParser
{
    public class Constants
    {
        public static readonly Regex OrderLinePattern =
            new Regex("^([0-9]+x){1}\\W+(#[0-9]+|[A-Za-z]{1}[A-Za-z ]*){1}\\W*$", RegexOptions.Multiline);
    }
}
