using Ecom_Application.DAO;
using Ecom_Application.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce_System.Util;

namespace Ecommerce_System
{
    public class Product_AddToCart_Test
    {
        private OrderProcessorRepositoryImpl _repo;
        private Customers _customers;
        private Products _products;
        [SetUp]
        public void SetUp()
        {
            _repo = new OrderProcessorRepositoryImpl();
            string uniqueemail="email_"+ Guid.NewGuid().ToString("N").Substring(0,8)+"@example.com";
            _customers = new Customers
            {
                Name = "Test_Customer_1",
                Email = uniqueemail,
                Password = "password1"
            };
           bool CustomerCreated= _repo.CreateCustomer(_customers);
            Assert.That(CustomerCreated, Is.True,"Customer Creation - failed");
            _customers = _repo.Login(_customers.Email, _customers.Password);
           

            string uniqueporoductname = "P_Name" + Guid.NewGuid().ToString("N").Substring(0, 10); 
            _products = new Products
            {
                Name = uniqueporoductname,
                Price = 999.24f,
                Description = "Test  description",
                StockQuantity = 123
            };
            bool created= _repo.CreateProduct(_products);
            Assert.That(created, Is.True, "Product Creation Failed");
            // var tempList = _repo.GetAllFromCart(_customers);
            using (var con = DBconnection.GetConnection())
            {
                con.Open();
                var cmd = new SqlCommand("SELECT TOP 1 product_id FROM products WHERE name = @name ORDER BY product_id DESC", con);
                cmd.Parameters.AddWithValue("@name", _products.Name);
                var result = cmd.ExecuteScalar();
                Assert.That(result, Is.Not.Null, "Product ID fetch failed.");
                _products.Product_Id = Convert.ToInt32(result);
            }
        }
                
        [Test]
        public void AddToCart()
        {
            _repo.RemoveFromCart(_customers,_products);
            var result = _repo.AddToCart(_customers, _products, 10);
            Assert.That(result, Is.True, "AddToCart failed. Make sure the customer and product exist and are not already in the cart.");
        }
        
    }
}
