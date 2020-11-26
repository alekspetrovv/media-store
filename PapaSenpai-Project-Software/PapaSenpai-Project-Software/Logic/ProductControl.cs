using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class ProductControl
    {
        private List<Product> products;
        private ProductDAL productDAL;
        public ProductControl()
        {
            this.products = new List<Product>();
            this.productDAL = new ProductDAL();
        }


        public Product GetProductById(int id)
        {
            foreach (Product product in this.GetProducts())
            {
                if(product.Id == id)
                {
                    return product;
                }
            }
            return null;
        }

        public Product GetProductByTitle(string title)
        {
            foreach (Product product in this.GetProducts())
            {
                if (product.Title == title)
                {
                    return product;
                }
            }
            return null;
        }



        public void retrieveAllProducts()
        {
            MySqlDataReader products = productDAL.Select();
            this.EmptyProducts();
            if (products.HasRows)
            {
                while (products.Read())
                {
                    Product product = new Product(Convert.ToInt32(products["id"]), products["title"].ToString(),
                    products["description"].ToString(),(Convert.ToInt32(products["quantity"])), (Convert.ToInt32(products["quantitydepo"])), (Convert.ToDouble(products["selling_price"])),
                    (Convert.ToDouble(products["buying_price"])),(Convert.ToInt32(products["threshold"])), Convert.ToInt32(products["overall_price"]));

                    this.products.Add(product);
                }
            }
        }

        public void AddProduct(string[] product_bindings)
        {
            this.productDAL.Insert(product_bindings);
            this.retrieveAllProducts();
        }

        public void UpdateProduct(string[] product_bindings)
        {
            this.productDAL.Update(product_bindings);
            this.retrieveAllProducts();
        }

        public void DeleteProduct(string[] product_bindings)
        {
            this.productDAL.Delete(product_bindings);
        }

        private void EmptyProducts()
        {
            this.products.Clear();
        }


        public List<Product> GetProducts()
        {
            return this.products;
        }
    }
}
