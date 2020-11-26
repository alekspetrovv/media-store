using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class OrderProduct
    {
        private Product product;
        private int quanity;
        private double price;

        public OrderProduct(Product product, int quanity, double price)
        {
            this.product = product;
            this.quanity = quanity;
            this.price = price;
        }

        public Product Product
        {
            get { return this.product; }
        }
        public int Quantity 
        {
            get { return this.quanity; }
        }

    }
}
