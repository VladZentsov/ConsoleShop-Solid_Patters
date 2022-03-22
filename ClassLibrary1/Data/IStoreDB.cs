using ClassLibrary1.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Data
{
    public interface IStoreDB
    {

        /// <summary>Gets or sets the orders.</summary>
        /// <value>The orders.</value>
        public List<Order> Orders { get; set; }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public List<Product> Products { get; set; }

        /// <summary>Gets or sets the registered users.</summary>
        /// <value>The registered users.</value>
        public List<RegisteredUser> RegisteredUsers { get; set; }

        /// <summary>Gets or sets the administrators.</summary>
        /// <value>The administrators.</value>
        public List<Administrator> Administrators { get; set; }
    }
}
