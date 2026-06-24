using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Consts
{
    public record CommentEventProperty(
        Guid UserId,
        string comment,
        DateTime Timestamp
        )
    {
        public Guid UserId { get; init; } = UserId;
        public string comment {  get; set; }= comment;
        public DateTime Timestamp { get; set; }= Timestamp;
    }
}
