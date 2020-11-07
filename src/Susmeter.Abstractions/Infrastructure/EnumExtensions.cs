using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Susmeter.Abstractions.Infrastructure
{
    public static class EnumExtensions
    {
        public static TEnum ParseEnum<TEnum>(this string stringVal)
        {
            if (string.IsNullOrEmpty(stringVal))
                throw new ArgumentNullException(nameof(stringVal));

            foreach(var fi in typeof(TEnum).GetFields())
            {
                if (fi.GetCustomAttributes(typeof(HexValueAttribute), false).FirstOrDefault() is HexValueAttribute att)
                {
                    if (att.HexValue.Equals(stringVal, StringComparison.InvariantCultureIgnoreCase))
                        return (TEnum)fi.GetValue(null);
                }
            }

            throw new ArgumentException($"Could not parse {stringVal} to {typeof(Color).FullName}");
        }

        public static string HexValue<TEnum>(this TEnum enumVal)
            where TEnum : Enum
        {
            HexValueAttribute attribute = GetAttribute<TEnum, HexValueAttribute>(enumVal);
            return attribute.HexValue;
        }

        public static IEnumerable<TEnum> GetEnumValues<TEnum>()
            where TEnum : Enum
        {
            foreach (object item in Enum.GetValues(typeof(TEnum)))
            {
                yield return (TEnum)item;
            }
        }

        private static TAttribute GetAttribute<TEnum, TAttribute>(this TEnum enumValue)
            where TEnum : Enum
            where TAttribute : Attribute
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi == null)
                return null;

            TAttribute att = fi.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault() as TAttribute
                ?? throw new ArgumentException($"Could not get attribute {typeof(TAttribute).FullName} for enum {typeof(TEnum).FullName}");

            return att;
        }
    }
}
