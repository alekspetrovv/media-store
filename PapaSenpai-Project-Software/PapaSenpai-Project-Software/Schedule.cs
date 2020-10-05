using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Schedule
    {
        private int id;
        private string notes;
        private List<ScheduleMember> members;
        private DateTime date;

        public Schedule(int id, string notes, string date)
        {
            this.id = id;
            this.notes = notes;
            this.date = DateTime.Parse(date);
            this.members = new List<ScheduleMember>();
        }

        public List<ScheduleMember> Members
        {
            get { return members; }
            set { this.members = value; }
        }

        public int ID 
        {
            get { return this.id; }
        }


        public DateTime Date
        {
            get { return this.date; }
        }

        public void addMember(ScheduleMember member)
        {
            this.members.Add(member);
        }

    }
}
