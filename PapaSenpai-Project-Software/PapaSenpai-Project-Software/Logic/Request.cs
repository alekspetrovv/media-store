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
        private int productId;
        public Request(int id,int product_id,string quantity)
        {
            this.id = id;
            this.productId = product_id;
            this.quantity = quantity;
        }


        public int ProductID
        {
            get { return this.productId; }
        }

        public int Id
        {
            get { return this.id; }
        }


        public string Quantity
        {
            get { return this.quantity; }
        }
    }
}
