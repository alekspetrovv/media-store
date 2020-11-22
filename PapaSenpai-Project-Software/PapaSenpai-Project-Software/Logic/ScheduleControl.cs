using MySql.Data.MySqlClient;
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
        private List<ScheduleMember> scheduleMembers;
        private EmployeeControl e;
        public ScheduleControl()
        {
            schedules = new List<Schedule>();
            e = new EmployeeControl();
            scheduleMembers = new List<ScheduleMember>();
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
            MySqlDataReader schedules = DBcon.executeReader("SELECT schedules.* from schedules");

            if (schedules.HasRows)
            {
                this.emptySchedules();
                while (schedules.Read())
                {
                    Schedule schedule = new Schedule(Convert.ToInt32(schedules["id"]), schedules["notes"].ToString(), schedules["date"].ToString());
                    
                    string[] bindings = { schedule.ID.ToString() };
                    MySqlDataReader employees_ids_q = DBcon.executeReader("SELECT employee_id as id ,from_hour, to_hour FROM schedules_employees WHERE" +
                    " schedule_id = @schedule_id", bindings);

                    if (employees_ids_q.HasRows)
                    {
                        while (employees_ids_q.Read())
                        {
                            int id = Convert.ToInt32(employees_ids_q["id"]);
                            Employee foundEmployee = e.getEmployeeById(id);

                            if (foundEmployee != null)
                            {
                                ScheduleMember member = new ScheduleMember(foundEmployee, employees_ids_q["from_hour"].ToString(), employees_ids_q["to_hour"].ToString());
                                this.addScheduleMember(member);
                            }
                        }
                    }
                    this.addSchedule(schedule);
                }
            }
        }

        public Object UpdateSchedule(string[] schedule_bindings)
        {
            // to do
            return DBcon.executeScalar("INSERT INTO `schedules`(`notes`, `date`) VALUES (@notes,@date); SELECT LAST_INSERT_ID()", schedule_bindings);
        }

        public void DeleteSchedule(string[] schedule_bindings)
        {
            DBcon.executeNonQuery("DELETE FROM `schedules_employees` WHERE schedule_id = @id", schedule_bindings);
        }

        public void AssignSchedule(string[] schedule_bindings)
        {
            // to do
            DBcon.executeNonQuery("INSERT INTO `schedules_employees`(`schedule_id`, `employee_id`, `from_hour`, `to_hour`)" +
                       " VALUES (@schedule_id, @member_id, @from_hour, @to_hour)", schedule_bindings);
        }

        public void addScheduleMember(ScheduleMember member)
        {
            if(scheduleMembers.Count > 5)
            {
                throw new Exception("You can't add more than 5 members to a schedule");
            }

            this.scheduleMembers.Add(member);
        }


        public List<ScheduleMember> Members
        {
            get { return scheduleMembers; }
            set { this.scheduleMembers = value; }
        }


        public List<Schedule> getSchedules()
        {
            return this.schedules;
        }
    }
}
