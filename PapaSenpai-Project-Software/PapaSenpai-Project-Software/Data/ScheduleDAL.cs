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
        public Object Insert(string[] schedule_bindings)
        {
            return this.executeScalar("INSERT INTO `schedules`(`notes`, `date`) VALUES (@notes,@date); SELECT LAST_INSERT_ID()", schedule_bindings);
        }

        public Object Update(string[] schedule_bindings)
        {
            //update
            return new Object();
        }

        public void Delete(string[] schedule_bindings)
        {
            this.executeNonQuery("DELETE FROM `schedules_employees` WHERE schedule_id = @id", schedule_bindings);
            this.executeNonQuery("DELETE FROM `schedules` WHERE id = @id", schedule_bindings);
        }
        
        public MySqlDataReader Select()
        {
            return this.executeReader("SELECT schedules.* from schedules");
        }
    }
}
