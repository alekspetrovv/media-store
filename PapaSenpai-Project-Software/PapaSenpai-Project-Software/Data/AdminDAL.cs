﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class AdminDAL : BaseDAL
    {
        
        public void Insert(string[] admin_bindings)
        {
            this.executeReader("INSERT INTO `admins`(`username`, `password`, `first_name`, `last_name`, `email`, `role_id`)" +
                       "VALUES(@username,@password,@first_name,@last_name,@email,@role_id)", admin_bindings);
        }

        public void Update(string[] admin_bindings)
        {
            this.executeReader("UPDATE `admins` SET `username`= @usn,`password`= @password," +
                   "`first_name`= @firstname,`last_name`= @lastname,`email`= @email," +
                   "`role_id`= @roleid WHERE `id` = @id", admin_bindings);
        }

        public void Delete(string[] admin_bindings)
        {
            this.executeNonQuery("DELETE FROM `admins` WHERE `id` = @id", admin_bindings);
        }

        public MySqlDataReader Select()
        {
            return this.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
                 "INNER JOIN roles ON roles.id = admins.role_id GROUP by admins.id");
        }

        public MySqlDataReader Login(string[] admin_bindings)
        {
            return this.executeReader("SELECT admins.*, roles.title as role_title FROM `admins` " +
               "INNER JOIN roles ON roles.id = admins.role_id " +
               "WHERE `username` = @usn AND password = @pass", admin_bindings
               );
        }
    }

}
