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
      new Order {Id = 1, Name = "Cookie", Category = Categories.PantryStaples, Supplier = "Nestle", Quantity = 10000},
      new Order {Name = "Ham", Category = Categories.DairyAndEgg, Supplier = "Nestle", Weight = 0.5, Quantity = 100}
    };

    [HttpGet]
    public ActionResult<List<Order>> Get()
    {
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrderById(int id)
    {

      var order = orders.Find(o => o.Id == id);

      if (order == null)
      {
        return NotFound();
      }
      return Ok(order);

    }

    [HttpPost]
    public ActionResult<List<Order>> CreateNewOrder(Order newOrder)
    {
      try
      {
        orders.Add(newOrder);
        return Created("order", orders);
      }
      catch (Exception ex)
      {
        return BadRequest($"Error occurred while creating a new order. {ex}");
      }
    }
  }
}