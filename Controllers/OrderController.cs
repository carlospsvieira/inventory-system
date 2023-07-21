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
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
      _orderService = orderService;

    }

    [HttpGet]
    public ActionResult<List<Order>> Get()
    {
      var orders = _orderService.GetAllOrders();
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrderById(int id)
    {
      var order = _orderService.GetOrderById(id);

      if (order == null)
      {
        return NotFound();
      }

      return Ok(order);
    }

    [HttpPost]
    public ActionResult<List<Order>> CreateNewOrder(Order newOrder)
    {
      _orderService.CreateNewOrder(newOrder);

      var orders = _orderService.GetAllOrders();
      
      return Created("order", orders);
    }
  }
}