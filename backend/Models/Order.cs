using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace inventory_system.Models
{
  public class Order
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<OrderItem>? Items { get; set; }
    public bool Completed { get; set; } = false;


    [JsonIgnore]
    public DateTime EntryDate { get; set; }
    public string FormattedEntryDate => EntryDate.ToString("dd MMM yyyy HH:mm:ss");
  }
}
