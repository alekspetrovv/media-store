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
        private List<Department> departments;
        private DepartmentsDAL departmentsDAL;
        public DepartmentControl()
        {
            departments = new List<Department>();
            departmentsDAL = new DepartmentsDAL();
        }



        public Department GetDepartmentsByID(int id)
        {
            foreach (Department departments in GetDepartments())
            {
                if (departments.Id == id)
                {
                    return departments;
                }
            }
            return null;
        }


        public Department GetDepartmentsByTitle(string title)
        {
            foreach (Department departments in GetDepartments())
            {
                if (departments.Title == title)
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
                 

                    if (department["id"].ToString() != "" && department["title"].ToString() != "")
                    {
                        Department newDepartment = new Department(Convert.ToInt32(department["id"]), department["title"].ToString());
                        departments.Add(newDepartment);
                        newDepartment.EmployeesCount = Convert.ToInt32(department["employees_count"]);
                        newDepartment.ProductsCount = Convert.ToInt32(department["products_count"]);
                        if (department["overall_price"] != DBNull.Value)
                        {
                          newDepartment.TotalProductsRevenue = Convert.ToInt32(department["overall_price"]);
                        }
                        
                    }
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

        public List<Department> GetDepartments(Department department = null)
        {
            List<Department> newdep = new List<Department>(departments);
            if (department != null)
            {
                foreach (Department d in departments)
                {
                    if (d.Title != department.Title)
                    {
                        newdep.Remove(d);
                    }
                }
            }
            return newdep;
        }

    }
}
