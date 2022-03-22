using ClassLibrary1.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Entities
{
    public class Order: BaseEntity
    {
        public DateTime CreationTime { get; set; }
        public User Customer { get; set; }
        public List<Product> Products { get; set; }
        public Status Status { get; set; }

    }
}
