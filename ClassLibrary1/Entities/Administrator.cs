using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Entities
{
    public class Administrator: User
    {
        public override Roles GetRole
        {
            get
            {
                return Roles.Administrator;
            }
        }
    }
}
