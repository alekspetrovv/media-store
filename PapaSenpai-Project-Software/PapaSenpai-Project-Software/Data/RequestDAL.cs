using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class RequestDAL : BaseDAL
    {

        public void Create(string[] request_bindings)
        {
            executeNonQuery("INSERT INTO `requests`(`products_id`,`quantity`) " +
                "VALUES (@productid,@quantity)", request_bindings);
        }



        public void Delete(string[] request_bindings)
        {
            executeNonQuery("DELETE FROM `requests` WHERE `id` = @id", request_bindings);
        }


        public MySqlDataReader Select()
        {
            return executeReader("SELECT * FROM `requests`");
        }
    }
}
