using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Employee : User
    {
        private string adress;
        private string city;
        private string country;
        private string phone_number;
        private string wage;
        private string username;
        private string password;
        Gender gender;
        Department department;

        public Employee(int id,string first_name, string last_name, string email, string adress, string city, string country,
         string phone_number, string gender, string department, string wage, string username, string password) : base(id, first_name, last_name, email)
        {
            this.adress = adress;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            this.wage = wage;
            this.username = username;
            this.password = password;
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
        }

        public string UserName
        {
            get
            {
                return this.username;
            }
        }

        public string Password
        {
            get { return this.password; }
        }

        public string Adress
        {
            get { return this.adress; }
        }

        public string City
        {
            get { return this.city; }
        }

        public string Country
        {
            get { return this.country; }
        }

        public string PhoneNumber
        {
            get { return this.phone_number; }
        }

        public string Wage
        {
            get { return this.wage; }
        }

        public Enum Gender
        {
            get { return this.gender; }
        }

        public Enum Department
        {
            get { return department; }
        }

        public static void retrieveAllEmployees()
        {
            MySqlDataReader employees = DBcon.executeReader("SELECT employees.*, departments.title as department FROM `employees` " +
                "INNER JOIN departments ON departments.id = employees.department_id GROUP by employees.id");
            StoreControl.emptyUsers();
            if (employees.HasRows)
            {
                while (employees.Read())
                {
                    Employee employee = new Employee(Convert.ToInt32(employees["id"]), employees["first_name"].ToString(),
                        employees["last_name"].ToString(), employees["email"].ToString()
                        , employees["address"].ToString(), employees["city"].ToString(), employees["country"].ToString(),
                        employees["phone_number"].ToString(), employees["gender"].ToString(), employees["department"].ToString(), employees["wage_per_hour"].ToString(), employees["username"].ToString(), employees ["password"].ToString());

                    StoreControl.addEmployee(employee);
                }
            }
        }

        public static void AddEmployee(string[] employee_bindings)
        { 
            DBcon.executeNonQuery("INSERT INTO `employees`(`first_name`, `last_name`, `address`, `city`, `country`, `wage_per_hour`, `phone_number`, `gender`, `email`, `department_id`, `username`, `password`)" +
                              "VALUES(@first_name,@last_name,@address,@city,@country,@wage_per_hour,@phone_number,@gender,@email,@department_id,@username,@password)", employee_bindings);
            retrieveAllEmployees();
        }
        public static void UpdateEmployee(string[] employee_bindings)
        {
            DBcon.executeNonQuery("UPDATE `employees` SET `first_name`= @firstname,`last_name`= @secondname," +
                   "`address`= @adress,`city`= @city,`country`= @country,`phone_number`=@phonenumber,`gender`=@gender,`email`=@email" +
                   ",`department_id`= @departmentid,`wage_per_hour` = @wage,`username`= @username,`password`= @password WHERE id = @id", employee_bindings);
            retrieveAllEmployees();
        }
        public static void DeleteEmployee(string[] employee_bindings)
        {
            DBcon.executeNonQuery("DELETE FROM `employees` WHERE `id` = @id", employee_bindings);
        }

    }
}
