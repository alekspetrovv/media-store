using PapaSenpai_Project_Software.Data;
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
        private  OrdersDAL ordersDAL;
        private  OrdersProductsDAL ordersProductsDAL;

        public OrdersControl()
        {
            this.products = new List<OrderProduct>();
            this.ordersDAL = new OrdersDAL();
            this.ordersProductsDAL = new OrdersProductsDAL();
        }

        public List<OrderProduct> Products
        {
            get { return this.products; }
        }

        public void addProduct(Product product, int quanity)
        {
            OrderProduct productForOrder = new OrderProduct(product, quanity, product.SellingPrice);
            this.products.Add(productForOrder);
        }

        public void Buy(string[] bindings)
        {
            int order_id = Convert.ToInt32(this.ordersDAL.Insert(bindings));

            foreach (OrderProduct product in this.Products)
            {

                string[] product_bind = { order_id.ToString(), product.Product.Id.ToString(), product.Product.SellingPrice.ToString() };
                this.ordersProductsDAL.Insert(product_bind);
            }

            this.products.Clear();
        }
    }
}
