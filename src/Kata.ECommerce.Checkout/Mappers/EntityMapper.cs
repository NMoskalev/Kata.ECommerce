using AutoMapper;
using Kata.ECommerce.Core.Checkout.Entities;
using Kata.ECommerce.Core.Checkout.Models;

namespace Kata.ECommerce.Checkout.Mappers
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<LineItemEntity, ILineItem>().ConstructUsing(c => new StandardLineItem
            {
                ProductCode = c.ProductCode,
                Price = c.Price,
                Total = c.Total,
                SubTotal = c.SubTotal
            });
            CreateMap<ILineItem, LineItemEntity>();
            CreateMap<ShoppingCart, ShoppingCartEntity>().ReverseMap();
            CreateMap<Discount, DiscountEntity>().ReverseMap();
        }
    }
}
