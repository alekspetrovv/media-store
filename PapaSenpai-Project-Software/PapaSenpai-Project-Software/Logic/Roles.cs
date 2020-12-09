using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class Roles
    {
        public int Id { get; }
        private Departments department;

        public string Title { get; }

        public Roles(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public string getDepartmentName()
        {
            if (this.Department != null)
            {
                return this.Department.Title;
            }

            return "";
        }


        public Departments Department {
            get { return this.department; }
            set { this.department = value; }
        }

    }
}
