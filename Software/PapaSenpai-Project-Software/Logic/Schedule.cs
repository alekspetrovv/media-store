using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    public class Schedule
    {
        private int id;
        private string notes;
        private DateTime date;

        public Schedule(int id, string notes, string date)  
        {
            this.id = id;
            this.date = DateTime.ParseExact(date, "MM-dd-yyyy",null);
        }

        public int ID 
        {
            get { return this.id; }
        }


        public DateTime Date
        {
            get { return this.date; }
        }

    }
}
