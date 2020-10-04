using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class User
    {
        private string first_name;
        private string last_name;
        private string email;
        private int id;

        public User(string first_name,string last_name,string email,int id) 
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.id = id;
        }

        public string FirstName 
        {
            get { return this.first_name; }
        }
        public string LastName 
        {
            get { return this.last_name; }
        }
        public string Email
        {
            get { return this.email; }
        }
        public int ID 
        {
            get { return this.id; }
        }
    }
}
