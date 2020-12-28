using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    public class Admin : User
    {
        private Role role;
        private string username;
        private string password;

        public Admin(int id, string username, string first_name, string last_name, string email, string password) : base(id, first_name, last_name, email)
        {
            this.password = password;
            this.username = username;
        }

        public string getRoleName()
        {
            if (Role != null)
            {
                return this.Role.Title;
            }
            return "Empty";
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Username
        {
            get { return this.username; }
        }

        public string Password
        {
            get { return this.password; }
        }

        public string ShowDepartmentInfo()
        {
            return $"Hello: " + getFullName() + "!" + " You are currently seeing all the information about department: " + Role.Department + ".";
        }
        public string ShowWorkerInfo()
        {
            return $"Hello: " + getFullName() + "!";
        }
    }
}
