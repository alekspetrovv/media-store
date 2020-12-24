using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class Department
    {
        public Department(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public string Title { get; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
