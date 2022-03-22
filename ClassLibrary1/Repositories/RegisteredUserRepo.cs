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
    public class RegisteredUserRepo:IRegisteredUserRepo
    {
        private readonly IStoreDB StoreDB;

        public RegisteredUserRepo(IStoreDB storeDB)
        {
            StoreDB = storeDB;
        }

        /// <summary>Adds the specified entity.</summary>
        /// <param name="entity">The Registered User entity.</param>
        public void Add(RegisteredUser entity)
        {
            Validation(entity);
            StoreDB.RegisteredUsers.Add(entity);
        }

        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The Registered User  entity.</param>
        public void Delete(RegisteredUser entity)
        {
            StoreDB.RegisteredUsers.Remove(entity);
        }

        /// <summary>Deletes the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        public void DeleteById(int id)
        {
            if (!StoreDB.RegisteredUsers.Any(o => o.Id == id))
                throw new ShopException("No product with such id");
            StoreDB.RegisteredUsers.Remove(GetById(id));
        }

        /// <summary>Finds all.</summary>
        /// <returns>All Registered User entities</returns>
        public IEnumerable<RegisteredUser> FindAll()
        {
            return StoreDB.RegisteredUsers;
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Registered User entity</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No products with such id</exception>
        public RegisteredUser GetById(int id)
        {
            return StoreDB.RegisteredUsers.FirstOrDefault(o => o.Id == id) ?? throw new ShopException("No products with such id");
        }

        /// <summary>Updates the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public void Update(RegisteredUser entity)
        {
            Validation(entity);
            var oldRegisteredUser = StoreDB.RegisteredUsers.FirstOrDefault(o => o.Id == entity.Id);
            if (oldRegisteredUser != null)
            {
                int index = StoreDB.RegisteredUsers.IndexOf(oldRegisteredUser);
                StoreDB.RegisteredUsers[index] = entity;
            }
        }
        private void Validation(RegisteredUser registeredUser)
        {
            if (registeredUser == null)
                throw new ArgumentNullException("registeredUser");
            if (registeredUser.Surname == null)
                throw new ShopException("Surname can't be null");
            if (registeredUser.Name == null)
                throw new ShopException("Name can't be null");
            if (registeredUser.Email == null)
                throw new ShopException("Email can't be null");
            if (registeredUser.Pass == null)
                throw new ShopException("Password can't be null");
        }

        /// <summary>Checks the existence.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if Registered User with such id exists and false if not</returns>
        public bool CheckExistence(int id)
        {
            return StoreDB.RegisteredUsers.Any(a => a.Id == id);
        }
    }
}
