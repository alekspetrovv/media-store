using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class DepartmentsDAL : BaseDAL
    {
        public void Insert(string[] department_bindings)
        {
            executeNonQuery("INSERT INTO `departments`(`title`) VALUES(@title)", department_bindings);
        }

        public void Update(string[] department_bindings)
        {
            executeNonQuery("UPDATE `departments` SET `title` = @title WHERE `id` = @id", department_bindings);
        }


        public void Delete(string[] delete_bindings)
        {
            executeNonQuery("DELETE FROM `departments` WHERE `id` = @id", delete_bindings);
        }

        public MySqlDataReader Select()
        {
            return executeReader("SELECT *,(SELECT COUNT(employees.id) " +
                " FROM employees " +
                " WHERE employees.department_id = departments.id) as employees_count , " +
                " (SELECT COUNT(products.id) " +
                " FROM products " +
                " WHERE products.department_id = departments.id) as products_count , " +
                " (SELECT SUM(orders_products.price) " +
                 "FROM orders_products " +
                " INNER JOIN products ON products.id = orders_products.product_id" +
                " WHERE products.department_id = departments.id) as overall_price" +
                " FROM departments ");
        }
    }
}
