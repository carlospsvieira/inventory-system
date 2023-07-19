using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Categories Category { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public DateOnly EntryDate { get; set; }
    }
}