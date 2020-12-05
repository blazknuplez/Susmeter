using Susmeter.Abstractions.Models;
using System;

namespace Susmeter.Abstractions.Infrastructure
{
    public static class FilterExtensions
    {
        public static DateTime CutOffDate(this Period period)
            => period switch
            {
                Period.Weekly => DateTime.Now.AddDays(-7),
                Period.Monthly => DateTime.Now.AddDays(-30),
                Period.AllTime => DateTime.MinValue,
                _ => throw new ArgumentException(nameof(period))
            };

        public static int MinGames(this Period period)
            => period switch
            {
                Period.Weekly => 1,
                Period.Monthly => 5,
                Period.AllTime => 5,
                _ => throw new ArgumentException(nameof(period))
            };
    }
}
