using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class UserControl
    {
        private List<Admin> admins;
        private User loggedUser;
        public UserControl()
        {
           admins = new List<Admin>();
           loggedUser = null;
        }

        public User getloggedUser()
        {
            return loggedUser;
        }

        public void logUser(User user)
        {
            loggedUser = user;
        }

        public void addAdmin(Admin admin)
        {
            this.admins.Add(admin);
        }

        public void emptyAdmins()
        {
            this.admins.Clear();
        }

        public Admin getAdminById(int id)
        {

            foreach (Admin admin in getAdmins())
            {
                if (id == admin.ID)
                {
                    return admin;
                }
            }

            return null;
        }


        public void AddAdmin(string[] admin_bindings)
        {
            DBcon.executeReader("INSERT INTO `admins`(`username`, `password`, `first_name`, `last_name`, `email`, `role_id`)" +
                         "VALUES(@username,@password,@first_name,@last_name,@email,@role_id)", admin_bindings);
            this.retrieveAllAdmins();
        }

        public void UpdateAdmin(string[] admin_bindings)
        {
            MySqlDataReader updateEmployee = DBcon.executeReader("UPDATE `admins` SET `username`= @usn,`password`= @password," +
                   "`first_name`= @firstname,`last_name`= @lastname,`email`= @email," +
                   "`role_id`= @roleid WHERE `id` = @id", admin_bindings);
            this.retrieveAllAdmins();
        }

        public void DeleteAdmin(string[] admin_bindings)
        {
            DBcon.executeNonQuery("DELETE FROM `admins` WHERE `id` = @id", admin_bindings);
        }

        public void retrieveAllAdmins()
        {
            MySqlDataReader admins = DBcon.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
                  "INNER JOIN roles ON roles.id = admins.role_id GROUP by admins.id");
            this.emptyAdmins();
            if (admins.HasRows)
            {
                while (admins.Read())
                {
                    Admin admin = new Admin(Convert.ToInt32(admins["id"]), admins["username"].ToString(),
                        admins["role_title"].ToString(), admins["first_name"].ToString()
                        , admins["last_name"].ToString(), admins["email"].ToString(), admins["password"].ToString());

                   this.addAdmin(admin);
                }
            }
        }

        public Admin getAdminByUsername(string userName)
        {
            foreach (Admin a in getAdmins())
            {
                if (a.Username == userName)
                {
                    return a;
                }
            }
            return null;
        }

        public List<Admin> getAdmins()
        {
            return this.admins;
        }
    }
}
