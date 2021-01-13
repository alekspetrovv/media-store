using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class RoleControl : Interface
    {
        private List<Role> roles;
        private RolesDAL rolesDal;
        public RoleControl()
        {
            roles = new List<Role>();
            rolesDal = new RolesDAL();
        }

        public Role GetRoleById(int id)
        {
            foreach (Role r in roles)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            return null;
        }

        public Role GetRoleByDeparment(Department d)
        {
            foreach (Role role in roles)
            {
                if (role.Department.Id == d.Id)
                {
                    return role;
                }
            }
            return null;
        }

        public Role GetRoleByTitle(string title)
        {
            foreach (Role r in roles)
            {
                if (r.Title == title)
                {
                    return r;
                }
            }
            return null;
        }


        public void retrieveAllRoles()
        {
            MySqlDataReader role = rolesDal.Select();
            this.EmptyRoles();
            if (role.HasRows)
            {
                while (role.Read())
                {
                    Role newRole = new Role(Convert.ToInt32(role["id"]), role["title"].ToString());

                    if (role["department_id"].ToString() != "" && role["department_title"].ToString() != "")
                    {
                        Department department = new Department(Convert.ToInt32(role["department_id"]), role["department_title"].ToString());
                        newRole.Department = department;
                    }
                    roles.Add(newRole);
                }
            }
        }

        public void Create(string[] role_bindings)
        {
            rolesDal.Create(role_bindings);
            retrieveAllRoles();
        }

        public void CreateWithoutDepartment(string[] roles_bindings)
        {
            rolesDal.InsertWithoutDepartment(roles_bindings);
            retrieveAllRoles();
        }

        public void Update(string[] role_bindings)
        {
            rolesDal.Update(role_bindings);
            retrieveAllRoles();
        }

        public void UpdateWithoutDepartment(string[] role_bindings)
        {
            rolesDal.UpdateWithoutDepartment(role_bindings);
            retrieveAllRoles();
        }

        public void Delete(string[] role_bindings)
        {
            rolesDal.Delete(role_bindings);
            retrieveAllRoles();
        }


        private void EmptyRoles()
        {
            roles.Clear();
        }

        public List<Role> GetRoles()
        {
            return roles;
        }


    }
}
