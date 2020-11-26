using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    class OrdersControl
    {
        private List<OrderProduct> products;

        public OrdersControl()
        {
            this.products = new List<OrderProduct>();
        }

        public void addProduct(Product product, int quanity)
        {
            OrderProduct productForOrder = new OrderProduct(product, quanity, product.SellingPrice);
            this.products.Add(productForOrder);
        }

        public void buy()
        {
            //complete the transaction
        }
    }
}
