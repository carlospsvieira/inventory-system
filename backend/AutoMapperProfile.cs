using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Order, Order>();
      CreateMap<Product, Product>();
      CreateMap<OrderItem, Product>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
  }
}