using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class InventoryItemService : IInventoryItemService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public InventoryItemService(IMapper mapper, DataContext context)
    {
      _context = context;
      _mapper = mapper;

    }
    public async Task<ServiceResponse<List<InventoryItem>>> DeleteInventoryItem(int id)
    {
      var serviceResponse = new ServiceResponse<List<InventoryItem>>();

      try
      {
        var inventoryItem = await _context.InventoryItems.FirstOrDefaultAsync(p => p.Id == id);

        if (inventoryItem == null)
          throw new Exception($"InventoryItem with Id '{id}' was not found.");

        _context.InventoryItems.Remove(inventoryItem);

        await _context.SaveChangesAsync();

        var inventoryItems = await _context.InventoryItems.ToListAsync();

        serviceResponse.Data = inventoryItems;
        serviceResponse.Message = $"InventoryItem with Id '{id}' was deleted.";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<InventoryItem>>> GetAllInventoryItems()
    {
      var serviceResponse = new ServiceResponse<List<InventoryItem>>();

      try
      {
        var inventoryItems = await _context.InventoryItems.ToListAsync();
        serviceResponse.Data = inventoryItems;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<InventoryItem>> GetInventoryItemById(int id)
    {
      var serviceResponse = new ServiceResponse<InventoryItem>();

      try
      {
        var inventoryItem = await _context.InventoryItems.FirstOrDefaultAsync(p => p.Id == id);

        if (inventoryItem == null)
          throw new Exception($"InventoryItem with Id '{id}' was not found.");

        serviceResponse.Data = inventoryItem;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<InventoryItem>> UpdateInventoryItem(InventoryItem updatedInventoryItem)
    {
      var serviceResponse = new ServiceResponse<InventoryItem>();

      try
      {
        var inventoryItem = await _context.InventoryItems.FirstOrDefaultAsync(p => p.Id == updatedInventoryItem.Id);

        if (inventoryItem == null)
          throw new Exception($"InventoryItem with id '{updatedInventoryItem.Id}' was not found.");

        _mapper.Map(updatedInventoryItem, inventoryItem);

        inventoryItem.EntryDate = DateTime.Now;

        await _context.SaveChangesAsync();

        serviceResponse.Data = inventoryItem;
        serviceResponse.Message = $"InventoryItem with Id '{updatedInventoryItem.Id}' was updated.";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}