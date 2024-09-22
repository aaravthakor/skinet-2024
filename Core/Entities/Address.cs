using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Address : BaseEntity
    {
        public required string Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; }
        public required string City { get; set; } = string.Empty;
        public required string State { get; set; } = string.Empty;
        public required string PostalCode { get; set; } = string.Empty;
        public required string Country { get; set; } = string.Empty;
    }
}
