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
    public class Exception_Handling_Test
    {
        private OrderProcessorRepositoryImpl _repo;
        [SetUp]
        public void SetUp()
        {
            _repo = new OrderProcessorRepositoryImpl();
        }
        [Test]
        public void Customer_Check()
        {
            var fakecustomer = new Customers { Customer_id = -999 };
            var fakeproduct = new Products { Product_Id = -999 };

            bool result = _repo.AddToCart(fakecustomer, fakeproduct,1);
            Assert.That(result, Is.False,"Invalid Customer or Product Id");
        }
        [Test]
        public void Order_Check()
        {
            var fakecustomer = new Customers { Customer_id = -1 };
            var fakeproduct = new Products { Product_Id = -1, StockQuantity = 120 };
            var cartitems = new List<(Products products, int quantity)>
            {
                (fakeproduct, 1)
            };
            bool result = _repo.PlaceOrder(fakecustomer, cartitems, "Invalid Address");
            Assert.That(result, Is.False, "Place order failed with invalid customer or product ID");
        }
    }
}
