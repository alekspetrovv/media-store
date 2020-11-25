﻿using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
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
        private EmployeeDAL employeeDAL;

        public EmployeeControl()
        {
            employees = new List<Employee>();
            employeeDAL = new EmployeeDAL();
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
            MySqlDataReader employees = employeeDAL.Select();
            this.emptyEmployees();
            if (employees.HasRows)
            {
                while (employees.Read())
                {
                    Employee employee = new Employee(Convert.ToInt32(employees["id"]), employees["first_name"].ToString(),
                        employees["last_name"].ToString(), employees["email"].ToString()
                        , employees["address"].ToString(), employees["city"].ToString(), employees["country"].ToString(),
                        employees["phone_number"].ToString(), employees["gender"].ToString(),
                        employees["department"].ToString(), employees["wage_per_hour"].ToString(), employees["username"].ToString(), employees["password"].ToString());

                    this.employees.Add(employee);
                }
            }
        }

        public void AddEmployee(string[] employee_bindings)
        {
            employeeDAL.Insert(employee_bindings);
            this.retrieveAllEmployees();
        }
        public void UpdateEmployee(string[] employee_bindings)
        {
            employeeDAL.Update(employee_bindings);
            this.retrieveAllEmployees();
        }
        public void DeleteEmployee(string[] employee_bindings)
        {
            employeeDAL.Delete(employee_bindings);
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