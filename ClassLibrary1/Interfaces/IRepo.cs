using ClassLibrary1.Entities;
using ClassLibrary1.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        /// <summary>Finds all.</summary>
        /// <returns>All entities</returns>
        IEnumerable<TEntity> FindAll();
        /// <summary>Gets the entity by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity with this id</returns>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No products with such id</exception>
        TEntity GetById(int id);
        /// <summary>Adds the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);
        void Update(TEntity entity);
        /// <summary>Deletes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);
        /// <summary>Deletes the entity by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ClassLibrary1.Validation.ShopException">No product with such id</exception>
        void DeleteById(int id);
        /// <summary>Checks the existence.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if entity with such id exists and false if not</returns>
        bool CheckExistence(int id);

    }
}
