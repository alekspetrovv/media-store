using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class ScheduleMember
    {
        public DateTime startTime;
        public DateTime endTime;
        public Employee employee;

        public ScheduleMember(Employee employee, string startDate, string endDate)
        {
            this.employee = employee;
            this.startTime = DateTime.Parse(startDate);
            this.endTime = DateTime.Parse(endDate);
        }
    }
}
