using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class StoreControl
    {
        private List<User> users;

        public StoreControl()
        {
            users = new List<User>();
        }

        public List<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                users = value;
            }
        }
    }
}
