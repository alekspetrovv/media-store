using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class EmployeeControl
    {
        private List<Employee> employees;


        public EmployeeControl()
        {
            employees = new List<Employee>();
        }

        public void addEmployee(Employee e)
           {
            this.employees.Add(e);
        }

        public Employee getEmployeeById(int id)
        {

            foreach (Employee employee in getEmployees())
            {
                if (id == employee.ID)
                {
                    return employee;
                }
            }

            return null;
        }

        public void retrieveAllEmployees()
        {
            MySqlDataReader employees = DBcon.executeReader("SELECT employees.*, departments.title as department FROM `employees` " +
                "INNER JOIN departments ON departments.id = employees.department_id GROUP by employees.id");
            this.emptyEmployees();
            if (employees.HasRows)
            {
                while (employees.Read())
                {
                    Employee employee = new Employee(Convert.ToInt32(employees["id"]), employees["first_name"].ToString(),
                        employees["last_name"].ToString(), employees["email"].ToString()
                        , employees["address"].ToString(), employees["city"].ToString(), employees["country"].ToString(),
                        employees["phone_number"].ToString(), employees["gender"].ToString(), employees["department"].ToString(), employees["wage_per_hour"].ToString(), employees["username"].ToString(), employees["password"].ToString());

                    this.addEmployee(employee);
                }
            }
        }

        public void AddEmployee(string[] employee_bindings)
        {
            DBcon.executeNonQuery("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`, `wage_per_hour`, `phone_number`, `gender`, `email`, `department_id`, `username`, `password`)" +
                              "VALUES(@first_name,@last_name,@address,@city,@country,@wage_per_hour,@phone_number,@gender,@email,@department_id,@username,@password)", employee_bindings);
            this.retrieveAllEmployees();
        }
        public void UpdateEmployee(string[] employee_bindings)
        {
            DBcon.executeNonQuery("UPDATE `employees` SET `first_name`= @firstname,`last_name`= @secondname," +
                   "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email" +
                   ",`department_id`= @departmentid,`wage_per_hour` = @wage,`username`= @username,`password`= @password WHERE id = @id", employee_bindings);
            this.retrieveAllEmployees();
        }
        public void DeleteEmployee(string[] employee_bindings)
        {
            DBcon.executeNonQuery("DELETE FROM `employees` WHERE `id` = @id", employee_bindings);
            this.retrieveAllEmployees();
        }

        public Employee GetEmployeeByEmail(string email)
        {
            foreach (Employee employee in getEmployees())
            {
                if (employee.Email == email)
                {
                    return employee;
                }
            }
            return null;
        }

        public void emptyEmployees()
        {
            this.employees.Clear();
        }



        public List<Employee> getEmployees()
        {
            return this.employees;
        }
    }
}
