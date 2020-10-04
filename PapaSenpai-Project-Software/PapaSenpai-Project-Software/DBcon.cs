using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class DBcon
    {
        public static MySqlConnection getConnection() 
        {
            MySqlConnection con = new MySqlConnection(@"server=localhost;user id=root;password = 123456;database=papasenpai");
            return con;
        }
    }
}
