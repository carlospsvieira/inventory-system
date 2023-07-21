using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace inventory_system.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public Categories Category { get; set; }
    public string Supplier { get; set; } = "";
    public int Quantity { get; set; }

    [JsonIgnore]
    public DateTime EntryDate { get; set; }
    public string FormattedEntryDate => EntryDate.ToString("dd MMM yyyy HH:mm:ss");
  }
}
