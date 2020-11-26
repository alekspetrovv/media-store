using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Logic;
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
        private int wage;
        private string username;
        private string password;
        private int shifts_taken = 0;
        private int hours_worked = 0;
        Gender gender;
        Department department;
        Contract contract;

        public Employee(int id,string first_name, string last_name, string email, string adress, string city, string country,
         string phone_number, string gender, string department,string contract,string wage, string username, string password, string shifts_taken, string hours_worked = null) : base(id,
             first_name, last_name, email)
        {
            this.adress = adress;
            this.city = city;
            this.country = country;
            this.phone_number = phone_number;
            if (shifts_taken != "")
            {
                this.shifts_taken = Convert.ToInt32(shifts_taken);
            }

            if (hours_worked != "")
            {

                this.hours_worked = Convert.ToInt32(hours_worked);
            }
            this.wage = Convert.ToInt32(wage);
            Enum.TryParse(gender, out this.gender);
            Enum.TryParse(department, out this.department);
            Enum.TryParse(contract, out this.contract);
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

        public int Wage
        {
            get { return this.wage; }
        }

        public int HoursWorked 
        {
            get { return this.hours_worked; }
        }

        public int ShiftsTaken 
        {
            get { return this.shifts_taken; }
        }



        public Enum Gender
        {
            get { return this.gender; }

        }

        public Enum Department
        {
            get { return department; }
        }


        public Enum Contract
        {
            get { return this.contract; }
        }
    }
}
