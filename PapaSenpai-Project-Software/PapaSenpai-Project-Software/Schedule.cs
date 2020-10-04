using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Schedule
    {
        public int id;
        public string status;
        public List<ScheduleMember> members;
        public DateTime date;

        public Schedule(int id, string status, string date)
        {
            this.id = id;
            this.status = status;
            this.date = DateTime.Parse(date);
        }

        public List<ScheduleMember> Members
        {
            get { return members; }
            set { this.members = value; }
        }

    }
}
