using ClassLibrary1.Data;
using ClassLibrary1.Entities;
using ClassLibrary1.Validation;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class AdministratorRepo: IAdministratorRepo
    {
        private readonly IStoreDB StoreDB;

        public AdministratorRepo(IStoreDB storeDB)
        {
            StoreDB = storeDB;
        }

        /// <summary>Adds the specified entity.</summary>
        /// <param name="entity">The Administrator entity.</param>
        public void Add(Administrator entity)
        {
            Validation(entity);
            StoreDB.Administrators.Add(entity);
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The Administrator entity.</param>
        public void Delete(Administrator entity)
        {
            StoreDB.Administrators.Remove(entity);
        }

        /// <summary>Deletes the Administrator by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        public void DeleteById(int id)
        {
            if (!StoreDB.Administrators.Any(o => o.Id == id))
                throw new ShopException("No product with such id");
            StoreDB.Administrators.Remove(GetById(id));
        }

        /// <summary>Finds all.</summary>
        /// <returns>All Administrator entities</returns>
        public IEnumerable<Administrator> FindAll()
        {
            return StoreDB.Administrators;
        }


        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Administrator entity</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No products with such id</exception>
        public Administrator GetById(int id)
        {
            return StoreDB.Administrators.FirstOrDefault(o => o.Id == id) ?? throw new ShopException("No products with such id");
        }


        /// <summary>Updates the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Update(Administrator entity)
        {
            Validation(entity);
            var oldAdministrator = StoreDB.Administrators.FirstOrDefault(o => o.Id == entity.Id);
            if (oldAdministrator != null)
            {
                int index = StoreDB.Administrators.IndexOf(oldAdministrator);
                StoreDB.Administrators[index] = entity;
            }
        }

        private void Validation(Administrator administrator)
        {
            if (administrator == null)
                throw new ArgumentNullException("administrator");
            if (administrator.Surname == null)
                throw new ShopException("Surname can't be null");
            if (administrator.Name == null)
                throw new ShopException("Name can't be null");
            if (administrator.Email == null)
                throw new ShopException("Email can't be null");
            if (administrator.Pass == null)
                throw new ShopException("Password can't be null");
        }

        /// <summary>Checks the existence.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if Administrator with such id exists and false if not</returns>
        public bool CheckExistence(int id)
        {
            return StoreDB.Administrators.Any(a => a.Id == id);
        }
    }
}
