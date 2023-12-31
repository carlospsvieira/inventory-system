using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace inventory_system.Models
{
  public class Item
  {
    public string? Name { get; set; }
    public Categories Category { get; set; }
    public string Supplier { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    [JsonIgnore]
    public DateTime EntryDate { get; set; }
    public string FormattedEntryDate => EntryDate.ToString("dd MMM yyyy HH:mm:ss");
  }
}