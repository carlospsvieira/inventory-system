using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Models
{
  public class Order : Product
  {
    private bool completed = false;

    public bool GetCompleted()
    {
      return completed;
    }

    public void SetCompleted(bool value) => completed = value;
  }
}