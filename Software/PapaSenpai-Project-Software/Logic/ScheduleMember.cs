using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    public class ScheduleMember
    {
        private DateTime startTime;
        private DateTime endTime;
        private Employee employee;

        public ScheduleMember(Employee employee, string startDate, string endDate)
        {
            this.employee = employee;
            //this.startTime = DateTime.ParseExact(date, "MM-dd-yyyy", null);
            this.endTime = DateTime.Parse(endDate);
            this.startTime = DateTime.Parse(startDate);
        }

        public Employee Employee 
        {
            get { return this.employee; }
        }

        public DateTime StartTime 
        {
            get { return this.startTime; }
        }

        public DateTime EndTime 
        {
            get { return this.endTime; }
        }
    }
}
