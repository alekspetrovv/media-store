using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    class ScheduleMemberDAL : BaseDAL
    {
        public void Insert(string[] schedule_bindings)
        {
            this.executeNonQuery("INSERT INTO `schedules_employees`(`schedule_id`, `employee_id`, `from_hour`, `to_hour`, `hours_worked`)" +
                         " VALUES (@schedule_id, @member_id, @from_hour, @to_hour, @hours_worked)", schedule_bindings); 
        }

        public void Delete(string[] schedule_bindings)
        {
            this.executeNonQuery("DELETE FROM `schedules_employees` WHERE schedule_id = @id", schedule_bindings);
        }
        
    }
}
