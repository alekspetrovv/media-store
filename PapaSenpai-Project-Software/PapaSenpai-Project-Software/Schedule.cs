using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class Schedule
    {
        private int id;
        private string notes;
        private List<ScheduleMember> members;
        private DateTime date;

        public Schedule(int id, string notes, string date)
        {
            this.id = id;
            this.notes = notes;
            this.date = DateTime.Parse(date);
            this.members = new List<ScheduleMember>();
        }

        public List<ScheduleMember> Members
        {
            get { return members; }
            set { this.members = value; }
        }

        public int ID 
        {
            get { return this.id; }
        }


        public DateTime Date
        {
            get { return this.date; }
        }

        public void addMember(ScheduleMember member)
        {
            this.members.Add(member);
        }

        public static void retrieveSchedules()
        {
            MySqlDataReader schedules = DBcon.executeReader("SELECT schedules.* from schedules");

            if (schedules.HasRows)
            {
                StoreControl.emptySchedules();
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
                            Employee foundEmployee = StoreControl.getEmployeeById(id);

                            if (foundEmployee != null)
                            {
                                ScheduleMember member = new ScheduleMember(foundEmployee, employees_ids_q["from_hour"].ToString(), employees_ids_q["to_hour"].ToString());
                                schedule.addMember(member);
                            }
                        }
                    }

                    StoreControl.addSchedule(schedule);
                }
            }


        }


    }
}
