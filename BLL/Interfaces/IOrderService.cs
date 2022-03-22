using BLL.Dto;
using ClassLibrary1.Enums;
using DAL.Enums;
using System;
using System.Collections.Generic;
using ClassLibrary1.Validation;
using System.Text;

namespace BLL.Interfaces
{
    public interface IOrderService:ICrud<OrderDto>
    {
        /// <summary>Gets the orders by user identifier.</summary>
        /// <param name="role">The role.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>All user Orders</returns>
        public IEnumerable<OrderDto> GetOrdersByUserId(Roles role, int customerId);
        /// <summary>Gets the price.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>Order price</returns>
        public decimal GetPrice(int orderId);
        /// <summary>Orderings the specified order identifier.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <exception cref="ShopException">Order already " + order.Status</exception>
        public void Ordering(int orderId);
        /// <summary>Sets the Order status.</summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="status">The status.</param>
        /// <exception cref="ShopException"></exception>
        public void SetStatus(int orderId, Status status);
        /// <summary>Gets the orders by ids.</summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Orders with such ids</returns>
        public IEnumerable<OrderDto> GetOrdersByIds(IEnumerable<int> ids);
    }
}
