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
            this.executeNonQuery("INSERT INTO `products`(`title`, `description`, `quantity`,`quantitydepo`,`selling_price`, `buying_price`, `threshold`)" +
                " VALUES (@title,@description,@quantity,@quantitydepo,@seling_price,@buying_price,@threshold)", products_bindings);
        }

        public void Update(string[] products_bindings)
        {
            this.executeNonQuery("UPDATE `products` SET `id`= @id,`title`= @title," +
                "`description`= @description,`quantity`= @quantity,`quantitydepo=@quantitydepo`,`selling_price`= @selling_price,`" +
                "buying_price`= @buying_price,`threshold`= @threshold", products_bindings);
        }

        public void Delete(string[] products_bindings)
        {
            this.executeNonQuery("DELETE FROM `products` WHERE `id` = @id", products_bindings); 
        }

        public MySqlDataReader Select()
        {
            return this.executeReader("SELECT * FROM `products`");
        }
    }
}
