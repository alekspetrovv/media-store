﻿using MySql.Data.MySqlClient;
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
        private string password;
        public Admin(int id, string username,string role,string first_name,string last_name,string email,string password) : base(id, first_name,last_name,email)
        {
            this.password = password;   
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

        public string Password 
        {
            get { return this.password; }
        }

        public static void retrieveAllAdmins()
        {
            MySqlDataReader admins = DBcon.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
                "INNER JOIN roles ON roles.id = admins.role_id GROUP by admins.id");
            StoreControl.emptyAdmins();
            if (admins.HasRows)
            {
                while (admins.Read())
                {
                    Admin admin = new Admin(Convert.ToInt32(admins["id"]), admins["username"].ToString(),
                        admins["role_title"].ToString(), admins["first_name"].ToString()
                        , admins["last_name"].ToString(), admins["email"].ToString(),admins["password"].ToString());

                    StoreControl.addAdmin(admin);
                }
            }
        }

    }
}