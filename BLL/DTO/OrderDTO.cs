using ClassLibrary1.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace BLL.Dto
{
    public class OrderDto:BaseDto
    {
        public DateTime CreationTime { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Roles CustomerRole { get; set; }
        public IEnumerable<int> ProductsIds { get; set; }
        public Status Status { get; set; }
    }
}
