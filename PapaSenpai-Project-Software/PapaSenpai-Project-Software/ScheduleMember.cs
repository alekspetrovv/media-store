using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class ScheduleMember
    {
        private DateTime startTime;
        private DateTime endTime;
        private Employee employee;

        public ScheduleMember(Employee employee, string startDate, string endDate)
        {
            this.employee = employee;
            this.startTime = DateTime.Parse(startDate);
            this.endTime = DateTime.Parse(endDate);
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
