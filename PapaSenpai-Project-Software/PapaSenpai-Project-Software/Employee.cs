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
        Gender gender;
        Department department;

        public Employee(int id, string first_name, string last_name, string email, string adress, string city, string country,
         string phone_number, string gender, string department, string wage) : base(id, first_name, last_name, email)
        {
            this.adress = adress;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            this.wage = wage;
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
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
                        employees["phone_number"].ToString(), employees["gender"].ToString(), employees["department"].ToString(), employees["wage_per_hour"].ToString());

                    StoreControl.addEmployee(employee);
                }
            }
        }


    }
}
