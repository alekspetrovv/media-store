using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Admin : User
    {
        private string password;
        private string username;
        private Role role;

        public Admin(string username,string password,Role role,string first_name,string last_name,string email,int id) : base(first_name,last_name,email,id)
        {
            this.password = password;
            this.role = role;
            this.username = username;
        }

        public string Password 
        {
            get { return this.password; }
        }

        public Role Role
        {
            get { return this.role; }
        }

    }
}
