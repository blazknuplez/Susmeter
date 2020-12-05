using Susmeter.Abstractions.Infrastructure;
using System;

namespace Susmeter.Abstractions.Models
{
    public class Filter
    {
        public Filter(Period period)
        {
            Period = period;
        }

        public Period Period { get; set; }

        public DateTime CutOffDate => Period.CutOffDate();

        public int MinGames => Period.MinGames();

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 3;

        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
    }
}
