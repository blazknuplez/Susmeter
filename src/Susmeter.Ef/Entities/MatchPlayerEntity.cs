using Susmeter.Abstractions.Models;

namespace Susmeter.Ef.Entities
{
    public class MatchPlayerEntity
    {
        public long MatchPlayerId { get; set; }

        public long MatchId { get; set; }

        public long PlayerId { get; set; }

        public string HexColor { get; set; }

        public Role PlayerRole { get; set; }

        public MatchEntity Match { get; set; }

        public PlayerEntity Player { get; set; }

        public ColorEntity Color { get; set; }
    }
}
