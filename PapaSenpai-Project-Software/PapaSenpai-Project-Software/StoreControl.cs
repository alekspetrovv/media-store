using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class StoreControl
    {
        private static List<User> users;
        private static User loggedUser;

        public StoreControl()
        {
            StoreControl.users = new List<User>();
            StoreControl.loggedUser = null;
        }

        public static List<User> getUsers()
        {
            return StoreControl.users;
        }

        public static User getloggedUser()
        {
            return StoreControl.loggedUser;
        }

        public static void logUser(User user)
        {
            StoreControl.loggedUser = user;
        }

    }
}
