using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_system.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InventoryItemController : ControllerBase
  {
    private readonly IInventoryItemService _inventoryItemService;
    public InventoryItemController(IInventoryItemService inventoryItemService)
    {
      _inventoryItemService = inventoryItemService;

    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<InventoryItem>>>> Get()
    {
      var inventoryItems = await _inventoryItemService.GetAllInventoryItems();

      if (inventoryItems.Success == false)
      {
        return BadRequest(inventoryItems);
      }

      return Ok(inventoryItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<InventoryItem>>> GetById(int id)
    {
      var inventoryItem = await _inventoryItemService.GetInventoryItemById(id);

      if (inventoryItem.Success == false)
      {
        return NotFound(inventoryItem);
      }

      return Ok(inventoryItem);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<InventoryItem>>>> DeleteInventoryItem(int id)
    {
      var inventoryItems = await _inventoryItemService.DeleteInventoryItem(id);

      if (inventoryItems.Success == false)
      {
        return NotFound(inventoryItems);
      }

      return Ok(inventoryItems);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ServiceResponse<InventoryItem>>> UpdateInventoryItem(InventoryItem updatedInventoryItem)
    {
      var inventoryItems = await _inventoryItemService.UpdateInventoryItem(updatedInventoryItem);

      if (inventoryItems.Success == false)
      {
        return NotFound(inventoryItems);
      }

      return Ok(inventoryItems);
    }

  }
}