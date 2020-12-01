using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class Product
    {
        private int id;
        private string title;
        private string description;
        private int quantity;
        private int quantityDepo;
        private double selling_price;
        private double buying_price;
        private double overall_price = 0;
        private int threshold;

        public Product(int id, string title, string description, int quantity, int quantity_depo, double sellingPrice, double buyingPrice, int threshHold, int overall_price)
        {
            this.Id = id;
            this.title = title;
            this.description = description;
            this.quantity = quantity;
            quantityDepo = quantity_depo;
            selling_price = sellingPrice;
            buying_price = buyingPrice;
            this.overall_price = overall_price;
            threshold = threshHold;
        }


        public int Id 
        {
            get { return this.id;  }
            set { this.id = value; }
            
        }

        public string Title
        {
            get { return this.title; }
        }   

        public string Description
        {
            get { return this.description; }
        }

        public int Quantity
        {
            get { return this.quantity; }
        }

        public int QuantityDepo
        {
            get { return this.quantityDepo; }
        }

        public double SellingPrice
        {
            get { return this.selling_price; }
        }

        public double BuyingPrice
        {
            get { return this.buying_price; }
        }

        public double OverallPrice 
        {
            get { return this.overall_price; }
        }
       
        public int ThreshHold
        {
            get { return this.threshold; }
        }

        public override string ToString()
        {
            return $"{this.Title}, Quanity - {this.Quantity}";
        }

    }
}
