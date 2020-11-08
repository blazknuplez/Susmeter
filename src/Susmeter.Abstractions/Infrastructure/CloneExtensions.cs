using Newtonsoft.Json;
using System;

namespace Susmeter.Abstractions.Infrastructure
{
    public static class CloneExtensions
    {
        public static T Clone<T>(this T source, Action<T> with = null)
        {
            var serialized = JsonConvert.SerializeObject(source);
            var item = JsonConvert.DeserializeObject<T>(serialized);
            with?.Invoke(item);

            return item;
        }
    }
}
