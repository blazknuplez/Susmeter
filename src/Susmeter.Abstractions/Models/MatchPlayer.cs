using Susmeter.Abstractions.Infrastructure;

namespace Susmeter.Abstractions.Models
{
    public class MatchPlayer : Player
    {
        public Role PlayerRole { get; set; }

        public string HexColor { get; set; }

        public Color Color
        {
            get
            {
                return HexColor?.ParseEnum<Color>() ?? default(Color);
            }
            set
            {
                HexColor = value.HexValue();
            }
        }
    }
}
