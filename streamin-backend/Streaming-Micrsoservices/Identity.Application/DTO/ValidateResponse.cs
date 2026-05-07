using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DTO
{
    public class ValidateResponse
    {
        public string? UserId { get; set; }

        public string? Hash { get; set; }

        public string? Error { get; set; }
    }
}
