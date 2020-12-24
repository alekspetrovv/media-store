using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class RolesDAL : BaseDAL
    {
        public void Insert(string[] roles_bindings)
        {
            executeNonQuery("INSERT INTO `roles`(`title`,`department_id`) VALUES(@title,@department)", roles_bindings);
        }

        public void Update(string[] roles_bindings)
        {
            executeNonQuery("UPDATE `roles` SET `title` = @title,`department_id` = @department WHERE `id` = @id", roles_bindings);
        }

        public void Delete(string[] delete_bindings)
        {
            executeNonQuery("DELETE FROM `roles` WHERE `id` = @id", delete_bindings);
        }

        public MySqlDataReader Select()
        {
            return executeReader("SELECT roles.*, departments.title as department_title FROM `roles` LEFT JOIN departments ON roles.department_id = departments.id");
        }

    }
}
