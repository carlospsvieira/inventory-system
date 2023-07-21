using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_system.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderController : ControllerBase
  {
    private static List<Order> orders = new List<Order> {
      new Order {Name = "Cookie", Category = Categories.PantryStaples, Supplier = "Nestle", Quantity = 10000},
      new Order {Name = "Ham pack 100g", Category = Categories.DairyAndEgg, Supplier = "Nestle", Quantity = 100}
    };

    [HttpGet]
    public ActionResult<List<Order>> Get()
    {
      return Ok(orders);
    }
  }
}