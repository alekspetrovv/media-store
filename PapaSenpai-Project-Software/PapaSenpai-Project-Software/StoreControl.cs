using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    static class StoreControl
    {
        private static List<Employee> users;
        private static List<Admin> admins;
        private static User loggedUser;

        static StoreControl()
        {
            StoreControl.users = new List<Employee>();
            StoreControl.admins = new List<Admin>();
            StoreControl.loggedUser = null;
        }

        public static List<Employee> getUsers()
        {
            return StoreControl.users;
        }

        public static List<Admin> getAdmins()
        {
            return StoreControl.admins;
        }


        public static User getloggedUser()
        {
            return StoreControl.loggedUser;
        }

        public static void logUser(User user)
        {
            StoreControl.loggedUser = user;
        }

        public static void addEmployee(Employee employee)
        {
            StoreControl.users.Add(employee);
        }

        public static void addAdmin(Admin admin)
        {
            StoreControl.admins.Add(admin);
        }


    }
}
