﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.Entity
{
    public class Order_items
    {
        public int order_item_id {  get; set; }
        public int order_id { get; set; }
        public int product_id {  get; set; }
        public int quantity { get; set; }

    }
}
