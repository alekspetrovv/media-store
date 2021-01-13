using MySql.Data.MySqlClient;
using PapaSenpai_Project_Software.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public class RequestsControl
    {
        private RequestDAL requestDAL;
        private ProductDAL productsDAL;
        private List<Request> requests;


        public RequestsControl()
        {
            requestDAL = new RequestDAL();
            productsDAL = new ProductDAL();
            requests = new List<Request>();
        }




        public void retrieveAllRequests()
        {
            MySqlDataReader request = requestDAL.Select();
            this.emptyRequests();
            if (request.HasRows)
            {
                while (request.Read())
                {
                    Request r = new Request(Convert.ToInt32(request["id"]), (request["products_id"]).ToString(), request["quantity"].ToString());
                       
                    this.requests.Add(r);
                }
            }
        }

        public void Approve(string[] bindings)
        {
            this.productsDAL.UpdateQuantity(bindings);
        }




        public void Create(string[] request_bindings)
        {
            this.requestDAL.Create(request_bindings);
            this.retrieveAllRequests();
        }


        public void Delete(string [] request_bindings)
        {
            this.requestDAL.Delete(request_bindings);
            this.retrieveAllRequests();
        }

        public void emptyRequests()
        {
            this.requests.Clear();
        }


        public List<Request> GetRequests()
        {
            return this.requests;
        }
    }
}
