using Ecom_Application.DAO;
using Ecom_Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom_Application.myexceptions;


namespace Ecom_Application
{
    public class EcomApp
    {
        static void Main(string[] args)
        {
            OrderProcessorRepositoryImpl repo=new OrderProcessorRepositoryImpl();
            bool check = false;

            while (!check)
            {
                Console.WriteLine("\n  E-COMMERCE APLLICATION  ");
                Console.WriteLine("\n PLEASE CHECK OUT THE MENU");
                Console.WriteLine("0. Login");
                Console.WriteLine("1. Register Customer");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4, Delete Customer");
                Console.WriteLine("5. Add to cart");
                Console.WriteLine("6. Remove from cart");
                Console.WriteLine("7. View cart");
                Console.WriteLine("8. Place Order");
                Console.WriteLine("9. View Customer Order");
                Console.WriteLine("10.View Products(EF)");
                Console.WriteLine("11.Exit");
                Console.WriteLine(" Enter the choice:");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        try
                        {
                            Console.Write("Enter Email: ");
                            string emailid = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string pass = Console.ReadLine();
                            Customers loggedInCustomer = repo.Login(emailid, pass); 
                            Console.WriteLine($" Login successful! Welcome, {loggedInCustomer.Name} (ID: {loggedInCustomer.Customer_id}).");
                        }
                        catch 
                        {
                                throw new My_Exceptions.CustomerNotFoundExcepetion ("Invalid email or password. Please try again.");
                        }
                        break;

                    case "1":
                        Console.WriteLine("Enter Your Name: ");
                        string name=Console.ReadLine();
                        Console.WriteLine("Enter Your EmailID:");
                        string email=Console.ReadLine();
                        Console.WriteLine("Enter you Password: ");
                        string password=Console.ReadLine();
                        Customers new_customer = new Customers { Name = name, Email = email, Password = password};
                        bool result= repo.CreateCustomer(new_customer);
                        break;
                    case "2":
                        Console.Write("Enter Product Name: ");
                        string pname = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter Description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter Stock Quantity: ");
                        int stockquantity =Convert.ToInt32(Console.ReadLine());
                        Products new_product = new Products { Name = pname, Price = (float)price, Description = description, StockQuantity = stockquantity };
                        result = repo.CreateProduct(new_product);
                        break;
                    case "3":
                        repo.PrintAllProducts();
                        Console.Write("Enter Product ID to delete: ");
                        int productid=Convert.ToInt32(Console.ReadLine());
                        result = repo.DeleteProduct(productid);
                        break;
                    case "4":
                        Console.WriteLine("Enter the Customer ID: ");
                        int id=Convert.ToInt32(Console.ReadLine());
                        repo.DeleteCustomer(id);
                        break;
                    case "5":
                        repo.PrintAllProducts();
                        Console.WriteLine("Enter  Customer ID: ");
                        int cust_id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Product ID : ");
                        int product_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the Quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Customers customer = new Customers { Customer_id = cust_id };
                        Products product = new Products { Product_Id = product_id };
                        result = repo.AddToCart(customer, product, quantity);
                        break;
                    case "6":
                        Console.WriteLine("Enter Customer Id: ");
                        int customer_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Product Id: ");
                        int prod_id = Convert.ToInt32(Console.ReadLine());
                        Customers cust = new Customers { Customer_id = customer_id };
                        Products prod = new Products { Product_Id = prod_id };
                        result = repo.RemoveFromCart(cust, prod);
                        break;
                    case "7":
                        Console.WriteLine("Enter Customer Id: ");
                        int cusid = Convert.ToInt32(Console.ReadLine());
                        Customers custid = new Customers { Customer_id=cusid };
                        List<Products> cartItems = repo.GetAllFromCart(custid);
                        break;                                           
                    case "8":
                        Console.WriteLine("Enter Customer Id: ");
                        int customerID= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Product Id: ");
                        int Prod_id= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Quantity: ");
                        int quant= Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Shipping address: ");
                        string address= Console.ReadLine();
                        Customers Customer = new Customers { Customer_id = customerID };
                        Products Prod = new Products { Product_Id = Prod_id };
                        List<(Products, int)> items = new List<(Products, int)>
                        {
                        (Prod, quant)
                        };
                        result = repo.PlaceOrder(Customer, items, address);
                        Console.WriteLine(result ? "Order Placed Successfully!" : "Order Failed!");
                        break ;
                    case "9":
                        Console.WriteLine("Enter CustomerId: ");
                        int cid = Convert.ToInt32(Console.ReadLine());
                        Customers cusID = new Customers { Customer_id = cid };
                        repo.ViewCustomerOrders(cusID);
                        break;
                    case "10":
                        Console.WriteLine("All Products using Entity Framework");
                        List<Products> pro = repo.ShowProducts();
                        Console.WriteLine($"{"Product ID",-15} {"Name",-20} {"Price",10}");
                        foreach (var pr in pro)
                        {
                            Console.WriteLine($"ID: {pr.Product_Id}, Name: {pr.Name}, Price: ₹{pr.Price}");
                        }
                        break;
                    case "11":
                        Console.WriteLine("Thank you for using the E-Commerce Appication");
                        Environment.Exit(0);
                        break;         
                }
            }
        }
    }
}
