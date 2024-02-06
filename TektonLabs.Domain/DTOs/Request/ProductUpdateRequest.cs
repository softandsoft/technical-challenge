using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TektonLabs.Domain.DTOs.Request
{
    public class ProductUpdateRequest
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string ModificationUser { get; set; }
    }
}
