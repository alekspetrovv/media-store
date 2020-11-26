using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class EmployeeDAL : BaseDAL
    {
       
        public void Insert(string[] employee_bindings)
        {
            this.executeNonQuery("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`," +
                " `wage_per_hour`, `phone_number`, `gender`, `email`, `department_id`,`contract_id`, `username`, `password`)" +
                             "VALUES(@first_name,@last_name,@address,@city,@country,@wage_per_hour,@phone_number,@gender,@email,@department_id,@contract_id,@username,@password)", employee_bindings);
        }

        public void Update(string[] employee_bindings)
        {
 
            this.executeNonQuery("UPDATE `employees` SET `first_name`= @firstname,`last_name`= @secondname," +
                 "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email," +
                 "`department_id`= @departmentid,`contract_id` = @contractid,`wage_per_hour` = @wage,`username`= @username,`password`= @password WHERE id = @id", employee_bindings);
        }

        public void Delete(string[] employee_bindings)
        {
            this.executeNonQuery("DELETE FROM `employees` WHERE `id` = @id", employee_bindings);
        }

        public MySqlDataReader Select()
        {
                   return this.executeReader("SELECT employees.*, departments.title as department , contracts.title as contract, COUNT(schedules_employees.id) as shifts_taken, SUM(schedules_employees.hours_worked) as hours_worked" +
                       " FROM employees " +
                       "INNER JOIN departments ON departments.id = employees.department_id " +
                       "INNER JOIN contracts ON contracts.id = employees.contract_id " +
                       "LEFT JOIN schedules_employees ON schedules_employees.employee_id = employees.id" +
                       " GROUP BY employees.id");
        }

    }
}
