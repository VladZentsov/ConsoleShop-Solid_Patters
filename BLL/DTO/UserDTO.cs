using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Dto
{
    public class UserDto:BaseDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Pass { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<int> OrderIds { get; set; }
        public virtual Roles GetRole
        {
            get
            {
                return Roles.User;
            }
        }
    }
}
