using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class ScheduleControl
    {
        private List<Schedule> schedules;
        private ScheduleDAL scheduleDAL;
        private ScheduleMemberDAL scheduleMemberDAL;

        public ScheduleControl()
        {
            this.scheduleDAL = new ScheduleDAL();
            this.scheduleMemberDAL = new ScheduleMemberDAL();
            this.schedules = new List<Schedule>();
        }


        public void addSchedule(Schedule s)
        {
            this.schedules.Add(s);
        }

        public void emptySchedules()
        {
            this.schedules.Clear();
        }

        public Schedule getScheduleByDate(DateTime date)
        {
            foreach (Schedule schedule in getSchedules())
            {

                if (schedule.Date.Date == date.Date)
                {
                    return schedule;
                }
            }

            return null;
        }

        public void retrieveSchedules()
        {
            MySqlDataReader schedules = scheduleDAL.Select();

            if (schedules.HasRows)
            {
                this.emptySchedules();
                while (schedules.Read())
                {
                    Schedule schedule = new Schedule(Convert.ToInt32(schedules["id"]), schedules["notes"].ToString(), schedules["date"].ToString());

                    string[] bindings = { schedule.ID.ToString() };
                    MySqlDataReader employees_ids_q = scheduleDAL.executeReader("SELECT employee_id as id ,from_hour, to_hour FROM schedules_employees WHERE" +
                    " schedule_id = @schedule_id", bindings);

                    if (employees_ids_q.HasRows)
                    {
                        while (employees_ids_q.Read())
                        {
                            int id = Convert.ToInt32(employees_ids_q["id"]);
                            try  {
                            ScheduleMember member = new ScheduleMember(id, employees_ids_q["from_hour"].ToString(), employees_ids_q["to_hour"].ToString());
                            schedule.addScheduleMember(member);
                            } catch (Exception e)
                            {
                                //in case something is wrong with the dates again
                            }
                        }
                    }
                    this.addSchedule(schedule);
                }
            }
        }

        public Object Insert(string[] schedule_bindings)
        {
            Object id = scheduleDAL.Insert(schedule_bindings);
            return id;
        }

        public void Delete(string[] schedule_bindings)
        {
            scheduleDAL.Delete(schedule_bindings);
        }

        public void InsertMember(string[] schedule_bindings)
        {
            scheduleMemberDAL.Insert(schedule_bindings);
        }


        public List<Schedule> getSchedules()
        {
            return this.schedules;
        }

 }
}
