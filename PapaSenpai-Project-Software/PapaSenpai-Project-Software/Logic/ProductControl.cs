using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class ProductControl : Interface
    {
        private List<Product> products;
        private ProductDAL productDAL;
        public ProductControl()
        {
            products = new List<Product>();
            productDAL = new ProductDAL();
        }


        public Product GetProductById(int id)
        {
            foreach (Product product in this.GetProducts())
            {
                if (product.Id == id)
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
                    int overall = 0;

                    if (products["overall_price"] != System.DBNull.Value)
                    {
                        overall = Convert.ToInt32(products["overall_price"]);
                    }

                    Product product = new Product(Convert.ToInt32(products["id"]), products["title"].ToString(),
                    products["description"].ToString(), (Convert.ToInt32(products["quantity"])), (Convert.ToInt32(products["quantitydepo"])), (Convert.ToDouble(products["selling_price"])),
                    (Convert.ToDouble(products["buying_price"])), (Convert.ToInt32(products["threshold"])), overall);
                    this.products.Add(product);

                    if (products["department_id"].ToString() != "" && products["department_title"].ToString() != "")
                    {
                        Department department = new Department(Convert.ToInt32(products["department_id"]), products["department_title"].ToString());
                        product.Department = department;
                    }
                }
            }
        }

        public void Create(string[] product_bindings)
        {
            this.productDAL.Create(product_bindings);
            this.retrieveAllProducts();
        }

        public void Update(string[] product_bindings)
        {
            this.productDAL.Update(product_bindings);
            this.retrieveAllProducts();
        }

        public void Delete(string[] product_bindings)
        {
            this.productDAL.Delete(product_bindings);
        }

        private void EmptyProducts()
        {
            this.products.Clear();
        }


        public int GetProductsCount()
        {
            return this.products.Count;
        }


        public List<Product> GetProducts(Department department = null)
        {
            List<Product> productdep = new List<Product>(this.products);
            if (department != null)
            {
                foreach (Product product in this.products)
                {
                    if (product.Department.Title != department.Title)
                    {
                        productdep.Remove(product);
                    }
                }
            }
            return productdep;
        }
    }
}
