using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.Entity
{
    public class Orders
    {
        public int Order_id {  get; set; }
        public int Customer_id { get; set; }
        public DateTime Order_Date {  get; set; }
        public float Total_Price {  get; set; }
        public string Shipping_address {  get; set; }

        }
}
