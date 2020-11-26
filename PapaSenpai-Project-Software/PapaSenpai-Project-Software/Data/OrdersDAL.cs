using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class OrdersDAL : BaseDAL
    {
        public Object Insert(string[] bindings)
        {
            return this.executeScalar("INSERT INTO `orders`(`user_id`)  VALUES (@user_id); SELECT LAST_INSERT_ID()", bindings);
        }

       
    }
}
