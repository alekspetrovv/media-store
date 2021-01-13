using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class AdminControl : Interface
    {
        private List<Admin> admins;
        private Admin loggedUser;
        private AdminDAL adminDAL;
        public AdminControl()
        {
            admins = new List<Admin>();
            adminDAL = new AdminDAL();
            loggedUser = null;
        }

        public Admin getloggedUser()
        {
            return loggedUser;
        }

        public void logUser(Admin admin)
        {
            loggedUser = admin;
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


        public void Create(string[] admin_bindings)
        {
            adminDAL.Create(admin_bindings);
            this.retrieveAllAdmins();
        }

        public void Update(string[] admin_bindings)
        {
            adminDAL.Update(admin_bindings);
            this.retrieveAllAdmins();
        }

        public void Delete(string[] admin_bindings)
        {
            adminDAL.Delete(admin_bindings);
            this.retrieveAllAdmins();
        }

        public void retrieveAllAdmins()
        {
            MySqlDataReader admins = adminDAL.Select();
            this.emptyAdmins();
            if (admins.HasRows)
            {
                while (admins.Read())
                {
                    Admin admin = new Admin(Convert.ToInt32(admins["id"]), admins["username"].ToString(),
                       admins["first_name"].ToString()
                        , admins["last_name"].ToString(), admins["email"].ToString(), admins["password"].ToString());

                    if (admins["role_id"].ToString() != "" && admins["role_title"].ToString() != "")
                    {
                        Role role = new Role(Convert.ToInt32(admins["role_id"]), admins["role_title"].ToString());
                        if (admins["department_id"].ToString() != "" && admins["department_title"].ToString() != "")
                        {
                            Department department = new Department(Convert.ToInt32(admins["department_id"]), admins["department_title"].ToString());
                            role.Department = department;
                        }
                        admin.Role = role;
                    }

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
