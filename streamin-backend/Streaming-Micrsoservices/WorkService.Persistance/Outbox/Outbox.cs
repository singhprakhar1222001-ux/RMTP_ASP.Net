using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Persistance.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string TypeOfEvent { get; set; }
        public bool IsError { get; set; }

        public DateTime OccuredOn { get; set; }
        public bool IsProcessed { get; set; }
        
    }
}
