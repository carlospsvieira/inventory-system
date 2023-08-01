using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public interface IInventoryItemService
  {
    Task<ServiceResponse<List<InventoryItem>>> GetAllInventoryItems();
    Task<ServiceResponse<InventoryItem>> GetInventoryItemById(int id);
    Task<ServiceResponse<InventoryItem>> UpdateInventoryItem(InventoryItem updatedInventoryItem);
    Task<ServiceResponse<List<InventoryItem>>> DeleteInventoryItem(int id);
  }
}