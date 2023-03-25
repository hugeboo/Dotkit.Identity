using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Persistence.Models
{
    [Table("user_claim")]
    public sealed class UserClaim
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("claim_type")]
        public string CliamType { get; set; } = string.Empty;

        [Column("claim_value")]
        public string? CliamValue { get; set; }
    }
}
