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

        public string Title { get; }

        public Roles(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
