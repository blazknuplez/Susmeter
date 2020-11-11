using Susmeter.Abstractions.Models;

namespace Susmeter.DataAccess.Models
{
    internal class RoleStatsQuery
    {
        public long PlayerId { get; set; }

        public string Nickname { get; set; }

        public Role WinningRole { get; set; }
    }
}
