using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class OrdersProductsDAL : BaseDAL
    {
        public void Insert(string[] bindings)
        {
            this.executeNonQuery("INSERT INTO `orders_products`(`user_id`,`product_id`,`price`)  VALUES (@user_id, @product_id, @price)", bindings);
        }

    }
}
