using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class Request
    {
        private int id;
        private string quantity;
        private string productID;
        public Request(int id,string products_id,string quantity)
        {
            this.id = id;
            this.productID = products_id;
            this.quantity = quantity;
        }


        public int Id
        {
            get { return this.id; }
        }


        public string Quantity
        {
            get { return this.quantity; }
        }

        public string ProductId 
        {
            get { return this.productID; }
        }


        public override string ToString()
        {
            return $"Request with id: '{this.id}', Requested Quantity: '{this.quantity}' , For Product with id: '{this.productID}'";
        }
    }
}
