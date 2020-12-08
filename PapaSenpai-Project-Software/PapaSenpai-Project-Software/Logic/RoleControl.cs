using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class RoleControl
    {
        private List<Roles> roles;
        private RolesDAL rolesDal;

        public RoleControl()
        {
            roles = new List<Roles>();
            rolesDal = new RolesDAL();
        }

        public Roles GetRoleById(int id)
        {
            foreach (Roles r in roles)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            return null;
        }

        public Roles GetRoleByTitle(string title)
        {
            foreach (Roles r in roles)
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
                    Roles newRole = new Roles(Convert.ToInt32(role["id"]), role["title"].ToString());
                    roles.Add(newRole);
                }
            }
        }

        public void Insert(string[] role_bindings)
        {
            rolesDal.Insert(role_bindings);
            retrieveAllRoles();
        }


        public void Update(string[] role_bindings)
        {
            rolesDal.Update(role_bindings);
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

        public List<Roles> GetRoles()
        {
            return roles;
        }

    }
}
