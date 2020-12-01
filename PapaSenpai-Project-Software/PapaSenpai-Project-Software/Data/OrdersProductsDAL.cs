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
           executeNonQuery("INSERT INTO `orders_products`(`order_id`,`product_id`,`price`)  VALUES (@order_id, @product_id, @price)", bindings);
        }

    }
}
