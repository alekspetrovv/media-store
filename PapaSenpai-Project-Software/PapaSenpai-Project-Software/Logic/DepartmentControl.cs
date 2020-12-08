using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class DepartmentControl
    {
        private List<Departments> departments;
        private DepartmentsDAL departmentsDAL;
        public DepartmentControl()
        {
            departments = new List<Departments>();
            departmentsDAL = new DepartmentsDAL();
        }


        public Departments GetDepartmentsByID(int id)
        {
            foreach (Departments departments in GetDepartments())
            {
                if (departments.Id == id)
                {
                    return departments;
                }
            }
            return null;
        }


        public Departments GetDepartmentsByTitle(string title)
        {
            foreach (Departments departments in GetDepartments())
            {
                if(departments.Title == title)
                {
                    return departments;
                }
            }
            return null;
        }

        public void retrieveAllDepartments()
        {
            MySqlDataReader department = departmentsDAL.Select();
            this.EmptyDepartments();
            if (department.HasRows)
            {
                while (department.Read())
                {
                    Departments newDepartment = new Departments(Convert.ToInt32(department["id"]), department["title"].ToString());
                    departments.Add(newDepartment);
                }
            }
        }


        public void Insert(string[] department_bindings)
        {
            departmentsDAL.Insert(department_bindings);
            retrieveAllDepartments();
        }


        public void Update(string[] department_bindings)
        {
            departmentsDAL.Update(department_bindings);
            retrieveAllDepartments();
        }

        public void Delete(string[] department_bindings)
        {
            departmentsDAL.Delete(department_bindings);
            retrieveAllDepartments();
        }


        public void EmptyDepartments()
        {
            departments.Clear();
        }

        public List<Departments> GetDepartments()
        {
            return departments;
        }

    }
}
