using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class ScheduleDAL : BaseDAL
    {
        public void Assign(string[] schedule_bindings)
        {
            this.executeNonQuery("INSERT INTO `schedules_employees`(`schedule_id`, `employee_id`, `from_hour`, `to_hour`)" +
                         " VALUES (@schedule_id, @member_id, @from_hour, @to_hour)", schedule_bindings); 
        }

        public Object Update(string[] schedule_bindings)
        {
            return this.executeScalar("INSERT INTO `schedules`(`notes`, `date`) VALUES (@notes,@date); SELECT LAST_INSERT_ID()", schedule_bindings);
        }

        public void Delete(string[] schedule_bindings)
        {
            this.executeNonQuery("DELETE FROM `schedules_employees` WHERE schedule_id = @id", schedule_bindings);
        }
        
        public MySqlDataReader Select()
        {
            return this.executeReader("SELECT schedules.* from schedules");
        }
    }
}
