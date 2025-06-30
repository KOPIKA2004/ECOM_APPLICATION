using Ecom_Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.DAO
{
    public interface IOrderProcessorRepository
    {
        bool CreateProduct(Products product);
        bool CreateCustomer(Customers customer);
        bool DeleteProduct(int productid);
        bool DeleteCustomer(int customerid);

        bool AddToCart(Customers customer, Products product, int quantity);
        bool RemoveFromCart(Customers customer, Products product);
        List<Products> GetAllFromCart(Customers customer);

        void PrintAllProducts();
        bool PlaceOrder(Customers customer, List<(Products products, int quantity)> items, string shippingAddress);
        void ViewCustomerOrders(Customers customer);

        Customers Login(string email, string password);
    }
}
