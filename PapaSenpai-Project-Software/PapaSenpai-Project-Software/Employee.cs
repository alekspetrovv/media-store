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
        private uint phone_number;
        Gender gender;

        Department department;
        public Employee(string first_name,string last_name,string email,int id,string adress,string city,string country,
         uint phone_number,string gender,string department) : base(first_name,last_name,email,id)
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

        public uint PhoneNumber
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
