using System;

namespace Susmeter.Abstractions.Infrastructure
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class HexValueAttribute : Attribute
    {
        public HexValueAttribute(string hexValue)
        {
            HexValue = hexValue;
        }

        public string HexValue { get; set; }
    }
}
