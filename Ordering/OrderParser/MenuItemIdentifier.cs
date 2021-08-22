using System;
namespace Ordering.OrderParser
{
    public class MenuItemIdentifier: IMenuItemIdentifier
    {
        public string Value { get; }

        public MenuItemIdentifier(string identifier)
        {
            Value = identifier;
        }

    }
}
