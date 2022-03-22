using ClassLibrary1.Data;
using ClassLibrary1.Entities;
using DAL.Interfaces;
using ClassLibrary1.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly IStoreDB StoreDB;

        public ProductsRepo(IStoreDB storeDB)
        {
            StoreDB = storeDB;
        }

        /// <summary>Adds the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Add(Product entity)
        {
            Validation(entity);
            StoreDB.Products.Add(entity);
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Product entity)
        {
            StoreDB.Products.Remove(entity);
        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        public void DeleteById(int id)
        {
            if (!StoreDB.Products.Any(o => o.Id == id))
                throw new ShopException("No product with such id");
            StoreDB.Products.Remove(GetById(id));
        }

        /// <summary>Finds all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public IEnumerable<Product> FindAll()
        {
            return StoreDB.Products;
        }


        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product entity</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No products with such id</exception>
        public Product GetById(int id)
        {
            return StoreDB.Products.FirstOrDefault(o => o.Id == id) ?? throw new ShopException("No products with such id");
        }

        /// <summary>Updates the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Update(Product entity)
        {
            Validation(entity);
            var oldOrder = StoreDB.Products.FirstOrDefault(o => o.Id == entity.Id);
            if (oldOrder != null)
            {
                int index = StoreDB.Products.IndexOf(oldOrder);
                StoreDB.Products[index] = entity;
            }
        }
        private void Validation(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (product.Name==null|| product.Name == "")
                throw new ShopException("Product Name cannot be empty");
            if (product.Price == 0)
                throw new ShopException("Price cannot be 0");
        }


        /// <summary>Checks the existence.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if Product with such id exists and false if not</returns>
        public bool CheckExistence(int id)
        {
            return StoreDB.Products.Any(a => a.Id == id);
        }
    }
}
