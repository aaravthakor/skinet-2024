using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CartItem
    {
        public int ProducId { get; set; }

        public required  string Productname { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public required string PictureUrl { get; set; }

        public required string Brand { get; set; }

        public required string Type { get; set; }
    }
}
