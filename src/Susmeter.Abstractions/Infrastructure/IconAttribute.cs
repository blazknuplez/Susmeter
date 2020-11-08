using System;

namespace Susmeter.Abstractions.Infrastructure
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class IconAttribute : Attribute
    {
        public IconAttribute(string iconClass)
        {
            IconClass = iconClass;
        }

        public string IconClass { get; set; }
    }
}
