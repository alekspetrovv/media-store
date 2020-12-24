using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Data
{
    public class ProductDAL : BaseDAL
    {
        public void Insert(string[] products_bindings)
        {
            executeNonQuery("INSERT INTO `products`(`department_id`,`title`, `description`, " +
                "`quantity`,`quantitydepo`,`selling_price`, `buying_price`, `threshold`)" +
                " VALUES (@department_id,@title,@description,@quantity,@quantitydepo,@seling_price,@buying_price,@threshold)", products_bindings);
        }

        public void Update(string[] products_bindings)
        {
            executeNonQuery("UPDATE `products` SET `department_id` = @department,`title`= @title," +
                "`description`= @description,`quantity`= @quantity," +
                "`quantitydepo`=@quantitydepo,`selling_price`= @selling_price," +
                "`buying_price`= @buying_price,`threshold`= @threshold WHERE `id` = @id", products_bindings);
        }

        public void UpdateQuantity(string[] products_bindings)
        {
            executeNonQuery("UPDATE `products` SET `quantity`= @quantity WHERE `id` = @id", products_bindings);
        }

        public void UpdateDepoQuantity(string[] products_bindings)
        {
            executeNonQuery("UPDATE `products` SET `quantitydepo`= @quantitydepo WHERE `id` = @id", products_bindings);
        }


        public void Delete(string[] products_bindings)
        {
            executeNonQuery("DELETE FROM `products` WHERE `id` = @id", products_bindings);
        }

        public MySqlDataReader Select()
        {
            return this.executeReader("SELECT products.*,departments.title as department_title,SUM(orders_products.price) as overall_price FROM `products` " +
                "INNER JOIN departments ON departments.id = products.department_id " +
                "LEFT JOIN orders_products ON orders_products.product_id = products.id " +
                "GROUP BY products.id");
        }
    }
}
