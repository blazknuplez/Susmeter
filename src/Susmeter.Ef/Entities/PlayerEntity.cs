using System.Collections.Generic;

namespace Susmeter.Ef.Entities
{
    public class PlayerEntity
    {
        public long PlayerId { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string AvatarHexColor { get; set; }

        public List<MatchPlayerEntity> Matches { get; set; }

        public ColorEntity AvatarColor { get; set; }
    }
}
