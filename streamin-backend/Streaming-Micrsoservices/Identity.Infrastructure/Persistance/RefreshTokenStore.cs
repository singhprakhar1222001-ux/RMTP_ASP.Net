using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistance
{
    public class RefreshTokenStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string TokenHash { get; set; }

        public DateTime ExpiresOn { get; set; }

        public bool IsRevoked { get; set; }=false;

        public string? ReplacedByHash { get; set; }
    }
}
