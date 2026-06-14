using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsError { get; set; }

        public DateTime OccuredOn { get; set; }
        public bool IsProcessed { get; set; }
    }
}
