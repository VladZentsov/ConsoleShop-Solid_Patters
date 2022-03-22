using ClassLibrary1.Data;
using ClassLibrary1.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClassLibrary1.Validation;

namespace DAL.Repositories
{
    public class OrderRepo: IOrderRepo
    {
        private readonly IStoreDB StoreDB;

        public OrderRepo(IStoreDB storeDB)
        {
            StoreDB = storeDB;
        }

        /// <summary>Adds the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Add(Order entity)
        {
            Validation(entity);
            StoreDB.Orders.Add(entity);
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The Order entity.</param>
        public void Delete(Order entity)
        {
            StoreDB.Orders.Remove(entity);
        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        public void DeleteById(int id)
        {
            if (!StoreDB.Orders.Any(o => o.Id == id))
                throw new ShopException("No product with such id");
            StoreDB.Orders.Remove(GetById(id));
        }

        /// <summary>Finds all.</summary>
        /// <returns>All Orders</returns>
        public IEnumerable<Order> FindAll()
        {
            return StoreDB.Orders;
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order entity</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No products with such id</exception>
        public Order GetById(int id)
        {
            return StoreDB.Orders.FirstOrDefault(o => o.Id == id) ?? throw new ShopException("No products with such id");
        }

        /// <summary>Updates the specified entity.</summary>
        /// <param name="entity">The Order entity.</param>
        public void Update(Order entity)
        {
            Validation(entity);
            var oldOrder = StoreDB.Orders.FirstOrDefault(o => o.Id == entity.Id);
            if (oldOrder != null)
            {
                int index = StoreDB.Orders.IndexOf(oldOrder);
                StoreDB.Orders[index] = entity;
            }
        }
        private void Validation(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            if (order.Products == null||order.Products.Count==0)
                throw new ShopException("can't be order without products");
            if (order.Customer == null)
                throw new ShopException("can't be order without customer");
        }

        /// <summary>Checks the existence.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if Order with such id exists and false if not</returns>
        public bool CheckExistence(int id)
        {
            return StoreDB.Orders.Any(a => a.Id == id);
        }
    }
}
