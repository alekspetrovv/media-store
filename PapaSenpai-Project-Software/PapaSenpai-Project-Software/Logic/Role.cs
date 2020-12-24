using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class Role
    {
        public int Id { get; }
        private Department department;

        public string Title { get; }

        public Role(int id, string title)
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

            return "Empty";
        }


        public Department Department
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
