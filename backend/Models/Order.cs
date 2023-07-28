using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Models
{
  public class Order : Product
  {
    public bool Completed { get; set; } = false;
  }
}
