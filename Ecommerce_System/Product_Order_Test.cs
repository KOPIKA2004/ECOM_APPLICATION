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
    public class  Product_Order_Test
    {
        private OrderProcessorRepositoryImpl _repo;
        private Customers _customers;
        private Products _products;
        [SetUp]
        public void SetUp()
        {
            _repo = new OrderProcessorRepositoryImpl();

            string uniquemail ="email_"+ Guid.NewGuid().ToString("N").Substring(0,8)+"@example.com";
            _customers = new Customers
            {
                Name = "Test_User",
                Email = uniquemail,
                Password = "password"
            };
            bool customerCreated = _repo.CreateCustomer(_customers);
            Assert.That(customerCreated, Is.True,"Customer Created - failed");
            _customers=_repo.Login(_customers.Email, _customers.Password);

            string uniqueProductName="Name_"+ Guid.NewGuid().ToString("N").Substring(0,10);
            _products = new Products
            {
                Name = uniqueProductName,
                Price = 999.9f,
                Description = "Test Description",
                StockQuantity = 245
            };
            bool productCreated = _repo.CreateProduct(_products);
            Assert.That(productCreated, Is.True, "Product creation failed.");

         // to get product id
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
        public void PlaceOrder_Success()
        {
            var cartItems = new List<(Products products, int quantity)>
            {
                (_products, 2)
            };
            _repo.RemoveFromCart(_customers, _products);
            _repo.AddToCart(_customers, _products, 2);
            var result = _repo.PlaceOrder(_customers, cartItems, "123, Sample Address, City");
            Assert.That(result, Is.True, "PlaceOrder should succeed with valid customer, products, and address.");
        }
    }
}
        