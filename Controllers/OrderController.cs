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
    private static Order order = new Order() { Id = 1, Name = "Cookie", Category = Categories.Pet, EntryDate = DateTime.Now };

    [HttpGet]
    public ActionResult Get()
    {
      return Ok(order);
    }
  }
}