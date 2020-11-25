using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PapaSenpai_Project_Software.Data
{
    public class BaseDAL
    {
        public MySqlConnection GetConnection()
        {
            MySqlConnection con = new MySqlConnection(@"Server=studmysql01.fhict.local;Uid=dbi444915;Database=dbi444915;Pwd=123456;");
            return con;
        }

        public MySqlCommand defaultDatabaseConnection(string sql, string[] bindings = null)
        {
            MySqlConnection con = this.GetConnection();
            MySqlCommand cmd = con.CreateCommand();
            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;


                List<string> fields = new List<string>();
                MatchCollection mcol = Regex.Matches(sql, @"@\b\S+?\b");

                foreach (Match m in mcol)
                {
                    fields.Add(m.ToString());
                }

                if (bindings != null)
                {
                    for (int i = 0; i < bindings.Length; i++)
                    {
                        cmd.Parameters.Add(fields[i], MySqlDbType.VarChar).Value = bindings[i];
                    }
                }

                return cmd;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }


        public MySqlDataReader executeReader(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteReader();

        }

        public Object executeScalar(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteScalar();
        }

        public Object executeNonQuery(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteNonQuery();
        }




        public void CloseConnection(MySqlDataReader con)
        {
            con.Close();
        }
    }
}
