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
        private int employee_id;
        private int hours;

        public ScheduleMember(int employee_id, string startDate, string endDate)
        {
            this.employee_id = employee_id;
            //this.startTime = DateTime.ParseExact(date, "MM-dd-yyyy", null);
            this.endTime = DateTime.Parse(endDate);
            this.startTime = DateTime.Parse(startDate);
            this.hours = this.endTime.Hour - this.startTime.Hour;
        }

        public int EmployeeId
        {
            get { return this.employee_id; }
        }

        public DateTime StartTime 
        {
            get { return this.startTime; }
        }

        public DateTime EndTime 
        {
            get { return this.endTime; }
        }

        public int Hours
        {
            get { return this.hours; }
        }

    }
}
