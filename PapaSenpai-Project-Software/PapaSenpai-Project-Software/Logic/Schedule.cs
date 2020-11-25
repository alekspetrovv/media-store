using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    public class Schedule
    {
        private int id;
        private string notes;
        private DateTime date;
        private List<ScheduleMember> scheduleMembers;

        public Schedule(int id, string notes, string date)  
        {
            this.id = id;
            this.date = DateTime.ParseExact(date, "MM-dd-yyyy",null);
            scheduleMembers = new List<ScheduleMember>();
        }

        public int ID 
        {
            get { return this.id; }
        }


        public DateTime Date
        {
            get { return this.date; }
        }

        public void addScheduleMember(ScheduleMember member)
        {
            if (scheduleMembers.Count > 5)
            {
                throw new Exception("You can't add more than 5 members to a schedule");
            }

            this.scheduleMembers.Add(member);
        }

        public ScheduleMember findEmployeeMemberById(int id)
        {
            ScheduleMember foundMember = null;

            foreach (ScheduleMember member in this.Members)
            {
                if (member.EmployeeId == id)
                {
                    foundMember = member; 
                }
            }

            return foundMember;
        }


        public List<ScheduleMember> Members
        {
            get { return scheduleMembers; }
            set { this.scheduleMembers = value; }
        }

    }
}
