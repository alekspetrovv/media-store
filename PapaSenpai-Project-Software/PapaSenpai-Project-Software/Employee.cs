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
        Gender gender;
        Department department;

        public Employee(int id, string first_name,string last_name,string email,string adress,string city,string country,
         string phone_number,string gender,string department) : base(id,first_name,last_name,email)
        {
            this.adress = adress;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            Enum.TryParse(gender,out this.gender);
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
