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
        private string username;

        public Admin(int id, string username, string role,string first_name,string last_name,string email) : base(id, first_name,last_name,email)
        {
            this.username = username;
            Enum.TryParse(role,out this.role);
        }


        public Role Role
        {
            get { return this.role; }
        }

        public string Username 
        {
            get { return this.username; }
        }


    }
}
