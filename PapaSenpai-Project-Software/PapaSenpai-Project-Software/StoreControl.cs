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
        private static List<Schedule> schedules;
        private static User loggedUser;

        static StoreControl()
        {
            StoreControl.users = new List<Employee>();
            StoreControl.admins = new List<Admin>();
            StoreControl.schedules = new List<Schedule>();
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

        public static List<Schedule> getSchedules()
        {
            return StoreControl.schedules;
        }




        public static User getloggedUser()
        {
            return StoreControl.loggedUser;
        }

        public static void logUser(User user)
        {
            StoreControl.loggedUser = user;
        }

        public static Admin getAdminByUsername(string userName) 
        {
            foreach(Admin a in getAdmins()) 
            {
                if(a.Username == userName) 
                {
                    return a;
                }
            }
            return null;
        }

        public static Schedule getScheduleByDate(DateTime date)
        {
            foreach (Schedule schedule in getSchedules())
            {

                if (schedule.Date.Date == date.Date )
                {
                    return schedule;
                }
            }

            return null;
        }

        public static Employee getEmployeeById(int id)
        {

            foreach (Employee employee in getUsers())
            {
                if (id == employee.ID)
                {
                    return employee;
                }
            }

            return null;
        }

        public static Employee GetEmployeeByEmail(string email)
        {
            foreach (Employee employee in getUsers())
            {
                if(employee.Email == email) 
                {
                    return employee;
                }
            }
            return null; 
        }

        public static void addEmployee(Employee employee)
        {
            StoreControl.users.Add(employee);
        }

        public static void addAdmin(Admin admin)
        {
            StoreControl.admins.Add(admin);
        }

        public static void addSchedule(Schedule schedule)
        {
            StoreControl.schedules.Add(schedule);
        }


        public static void emptyAdmins()
        {
            StoreControl.admins.Clear();
        }


        public static void emptyUsers()
        {
            StoreControl.users.Clear();
        }

        public static void emptySchedules()
        {
            StoreControl.schedules.Clear();
        }




    }
}
