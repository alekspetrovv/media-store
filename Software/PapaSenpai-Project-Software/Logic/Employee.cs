using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    public class Employee : User
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
         string phone_number, string gender, string department, string wage, string username, string password) : base(id,
             first_name, last_name, email)
        {
            this.adress = adress;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            this.wage = wage;
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
            this.username = username;
            this.password = password;
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

    }
}
