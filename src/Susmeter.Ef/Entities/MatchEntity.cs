using Susmeter.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Susmeter.Ef.Entities
{
    public class MatchEntity
    {
        public long MatchId { get; set; }

        public DateTime Timestamp { get; set; }

        public Role Winner { get; set; }

        public List<MatchPlayerEntity> Players { get; set; } = new List<MatchPlayerEntity>();
    }
}
