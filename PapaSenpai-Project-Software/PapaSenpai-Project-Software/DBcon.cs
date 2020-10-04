using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software
{
    class DBcon
    {
        public static MySqlConnection GetConnection() 
        {
            MySqlConnection con = new MySqlConnection(@"server=localhost;user id=root;password = 123456;database=papasenpai");
            return con;
        }

        public static MySqlDataReader executeReader(string sql, string[] bindings)
        {
            MySqlConnection con = DBcon.getConnection();
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;


            List<string> fields = new List<string>();
            MatchCollection mcol = Regex.Matches(sql, @"@\b\S+?\b");

            foreach (Match m in mcol)
            {
                fields.Add(m.ToString());
            }

            for (int i = 0; i < bindings.Length; i++)
            {
                cmd.Parameters.Add(fields[i], MySqlDbType.VarChar).Value = bindings[i];
            }

            return cmd.ExecuteReader();

        }

        public static void CloseConnection(MySqlDataReader con)
        {
            con.Close();
        }
    }
}
