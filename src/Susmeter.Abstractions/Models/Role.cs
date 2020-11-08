using Susmeter.Abstractions.Infrastructure;

namespace Susmeter.Abstractions.Models
{
    public enum Role : byte
    {
        [Icon("fa-tools")]
        Crewmate,

        [Icon("fa-fire")]
        Impostor
    }
}
