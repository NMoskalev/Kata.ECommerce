using AutoMapper;
using Kata.ECommerce.Core.Checkout.Dto;
using Kata.ECommerce.Core.Checkout.DTO;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Mappers
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<LineItemDto, ILineItem>().ConstructUsing(c => new StandardLineItem
            {
                ProductCode = c.ProductCode,
                Price = c.Price,
                Total = c.Total,
                SubTotal = c.SubTotal
            });
            CreateMap<ILineItem, LineItemDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<Discount, DiscountDto>().ReverseMap();
        }
    }
}
