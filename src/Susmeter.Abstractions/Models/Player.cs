using Susmeter.Abstractions.Infrastructure;

namespace Susmeter.Abstractions.Models
{
    public class Player
    {
        public long PlayerId { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string AvatarHexColor { get; set; }

        public Color AvatarColor
        {
            get
            {
                return AvatarHexColor.ParseEnum<Color>();
            }
            set
            {
                AvatarHexColor = value.HexValue();
            }
        }
    }
}
