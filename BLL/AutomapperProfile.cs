using AutoMapper;
using BLL.Dto;
using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BLL
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.ProductsIds, c => c.MapFrom(t => t.Products.Select(x => x.Id)))
                .ForMember(o => o.CustomerId, c => c.MapFrom(t => t.Customer.Id))
                .ForMember(o => o.CustomerRole, c => c.MapFrom(t => t.Customer.GetType().Name));

            CreateMap<(OrderDto, User, List<Product>), Order>()
                .ForMember(o => o.Customer, c => c.MapFrom(t => t.Item2))
                .ForMember(o => o.Products, c => c.MapFrom(t => t.Item3))
                .ForMember(o => o.Id, c => c.MapFrom(t => t.Item1.Id))
                .ForMember(o => o.CreationTime, c => c.MapFrom(t => t.Item1.CreationTime))
                .ForMember(o => o.Status, c => c.MapFrom(t => t.Item1.Status));

            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<RegisteredUser, RegisteredUserDto>()
                .ForMember(o => o.OrderIds, c => c.MapFrom(t=>t.Orders.Select(x => x.Id)))
                .ReverseMap();



            CreateMap<(RegisteredUserDto, IEnumerable<Order>), RegisteredUser>()
                .ForMember(o => o.Orders, c => c.MapFrom(t => t.Item2.Any() ? t.Item2 : new List<Order>()))
                .ForMember(o => o.Name, c => c.MapFrom(t => t.Item1.Name))
                .ForMember(o => o.Surname, c => c.MapFrom(t => t.Item1.Surname))
                .ForMember(o => o.Email, c => c.MapFrom(t => t.Item1.Email))
                .ForMember(o => o.Pass, c => c.MapFrom(t => t.Item1.Pass))
                .ForMember(o => o.Id, c => c.MapFrom(t => t.Item1.Id))
                .ForMember(o => o.CreatedDate, c => c.MapFrom(t => t.Item1.CreatedDate));

            CreateMap<Administrator, AdministratorDto>()
                .ForMember(o => o.OrderIds, c => c.MapFrom(t => t.Orders.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<(AdministratorDto, IEnumerable<Order>), Administrator>()
                .ForMember(o => o.Orders, c => c.MapFrom(t => t.Item2))
                .ForMember(o => o.Name, c => c.MapFrom(t => t.Item1.Name))
                .ForMember(o => o.Surname, c => c.MapFrom(t => t.Item1.Surname))
                .ForMember(o => o.Email, c => c.MapFrom(t => t.Item1.Email))
                .ForMember(o => o.Pass, c => c.MapFrom(t => t.Item1.Pass))
                .ForMember(o => o.Id, c => c.MapFrom(t => t.Item1.Id))
                .ForMember(o => o.CreatedDate, c => c.MapFrom(t => t.Item1.CreatedDate));
        }
    }
}
