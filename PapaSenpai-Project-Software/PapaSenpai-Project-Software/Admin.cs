using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Admin : User
    {
        private Role role;

        public Admin(int id, string role,string first_name,string last_name,string email) : base(id, first_name,last_name,email)
        {
            Enum.TryParse(role,out this.role);
        }


        public Role Role
        {
            get { return this.role; }
        }

    }
}
