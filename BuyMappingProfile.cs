using AutoMapper;
using BuyWebPage.Data.Entities;
using BuyWebPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyWebPage
{
    public class BuyMappingProfile : Profile
    {
        public BuyMappingProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(o => o.Id, Ex => Ex.MapFrom(vm => vm.OrderItemId))
                .ForMember(o => o.ProductId, Ex => Ex.MapFrom(vm => vm.Product.ProductId))
                .ReverseMap();
        }
    }
}
