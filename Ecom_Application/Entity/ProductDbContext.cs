﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.Entity
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext() :base("MyConnection")
            {}
        public DbSet<Products> Products { get; set; }
    }
}
