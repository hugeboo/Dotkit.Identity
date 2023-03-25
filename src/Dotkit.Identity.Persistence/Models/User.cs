using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.Identity.Persistence.Models
{
    [Table("user")]
    public sealed class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string UserName { get; set; } = string.Empty;

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_time")]
        public DateTime CreatedTime { get; set; }

        [Column("modified_time")]
        public DateTime? ModifiedTime { get; set; }

        [Column("created_by_id")]
        public int CreatedById { get; set; }

        [Column("modified_by_id")]
        public int? ModifiedById { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }
    }
}
