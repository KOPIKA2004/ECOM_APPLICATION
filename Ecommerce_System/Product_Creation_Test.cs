using Ecom_Application.DAO;
using Ecom_Application.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce_System
{
    public class Product_Creation_Test
    {
        private OrderProcessorRepositoryImpl _repo;
        [SetUp]
        public void SetUp()
        {
            _repo = new OrderProcessorRepositoryImpl();
        }
        [Test]
        public void Product_Creation()
        {
            var uniquename = "name_"+Guid.NewGuid().ToString("N").Substring(0,10);
            var product = new Products
            {
                Name = uniquename,
                Price = 1500,
                Description = "Test Description",
                StockQuantity = 35
            };
            var result = _repo.CreateProduct(product);

            Assert.That(result, Is.True, "Product creation failed.");


        }
    }
}
