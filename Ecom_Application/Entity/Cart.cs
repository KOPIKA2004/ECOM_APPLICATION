using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.Entity
{
    public class Cart
    {
        public int cart_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Product_Id { get; set; }
        public int quantity { get; set; }
    }
}
