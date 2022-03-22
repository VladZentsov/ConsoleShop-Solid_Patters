using BLL.Dto;
using ClassLibrary1.Enums;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Builders
{
    public class OrderDtoBuilder
    {
        private readonly OrderDto orderDto;
        public OrderDtoBuilder()=> orderDto=new OrderDto();

        public OrderDtoBuilder SetDateTime(DateTime date)
        {
            orderDto.CreationTime = date;
            return this;
        }

        public OrderDtoBuilder SetCustomerId(int id)
        {
            orderDto.CustomerId = id;
            return this;
        }

        public OrderDtoBuilder SetCustomerRole(Roles role)
        {
            orderDto.CustomerRole = role;
            return this;
        }

        public OrderDtoBuilder SetProductsIds(IEnumerable<int> ids)
        {
            orderDto.ProductsIds = ids;
            return this;
        }

        public OrderDtoBuilder SetStatus(Status status)
        {
            orderDto.Status = status;
            return this;
        }
        public OrderDto Build() => orderDto;
    }
}
