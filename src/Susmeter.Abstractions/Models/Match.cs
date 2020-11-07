using System;
using System.Collections.Generic;

namespace Susmeter.Abstractions.Models
{
    public class Match
    {
        public long MatchId { get; set; }

        public DateTime Timestamp { get; set; }

        public Role Winner { get; set; }

        public List<MatchPlayer> Players { get; set; } = new List<MatchPlayer>();
    }
}
