using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Entities
{
    public abstract class User:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Order> Orders { get; set; }
        public virtual Roles GetRole
        {
            get
            {
                return Roles.User;
            }
        }

    }
}
