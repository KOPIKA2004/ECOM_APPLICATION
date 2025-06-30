using Ecom_Application.DAO;
using Ecom_Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom_Application.myexceptions;
using System.Runtime.CompilerServices;


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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n~~~ E-COMMERCE APLLICATION ~~~  ");
                Console.ResetColor();
                Console.WriteLine("\n PLEASE CHECK OUT THE MENU");
                Console.WriteLine("0. Login");
                Console.WriteLine("1. Register Customer");
                Console.WriteLine("2. Create Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Add to cart");
                Console.WriteLine("6. Remove from cart");
                Console.WriteLine("7. View cart");
                Console.WriteLine("8. Place Order");
                Console.WriteLine("9. View Customer Order");
                Console.WriteLine("10.View Products(EF)");
                Console.WriteLine("11.Exit");
                Console.ForegroundColor= ConsoleColor.Blue;
                Console.WriteLine(" Enter the choice:");
                Console.ResetColor();
                string choice = Console.ReadLine();
                if (!int.TryParse(choice,out int choicenumber)|| choicenumber<0||choicenumber>11)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Please enter valid number: ");
                    Console.ResetColor();
                    continue;
                }
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
                            if (loggedInCustomer == null)
                            {
                                throw new My_Exceptions.CustomerNotFoundExcepetion("Invalid password or email");
                            }
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine($" Login successful! Welcome, {loggedInCustomer.Name} (ID: {loggedInCustomer.Customer_id}).");
                            Console.ResetColor();
                        }
                        catch (My_Exceptions.CustomerNotFoundExcepetion ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ResetColor();
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            Console.ResetColor();
                        }

                        break;


                    case "1":
                        Console.Write("Enter Your Name: ");
                        string name=Console.ReadLine();
                        Console.Write("Enter Your EmailID:");
                        string email=Console.ReadLine();
                        Console.Write("Enter you Password: ");
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
                        Console.Write("Enter the Customer ID: ");
                        int id=Convert.ToInt32(Console.ReadLine());
                        repo.DeleteCustomer(id);
                        break;
                    case "5":
                        repo.PrintAllProducts();
                        Console.Write("Enter  Customer ID: ");
                        int cust_id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Product ID : ");
                        int product_id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the Quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Customers customer = new Customers { Customer_id = cust_id };
                        Products product = new Products { Product_Id = product_id };
                        result = repo.AddToCart(customer, product, quantity);
                        break;
                    case "6":
                        Console.Write("Enter Customer Id: ");
                        int customer_id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Product Id: ");
                        int prod_id = Convert.ToInt32(Console.ReadLine());
                        Customers cust = new Customers { Customer_id = customer_id };
                        Products prod = new Products { Product_Id = prod_id };
                        result = repo.RemoveFromCart(cust, prod);
                        break;
                    case "7":
                        Console.Write("Enter Customer Id: ");
                        int cusid = Convert.ToInt32(Console.ReadLine());
                        Customers custid = new Customers { Customer_id=cusid };
                        List<Products> cartItems = repo.GetAllFromCart(custid);
                        break;                                           
                    case "8":
                        Console.Write("Enter Customer Id: ");
                        int customerID= Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Product Id: ");
                        int Prod_id= Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int quant= Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Shipping address: ");
                        string address= Console.ReadLine();
                        Customers Customer = new Customers { Customer_id = customerID };
                        Products Prod = new Products { Product_Id = Prod_id };
                        List<(Products, int)> items = new List<(Products, int)>
                        {
                        (Prod, quant)
                        };
                        result = repo.PlaceOrder(Customer, items, address);
                        Console.Write(result ? "Order Placed Successfully!" : "Order Failed!");
                        break ;
                    case "9":
                        Console.Write("Enter CustomerId: ");
                        int cid = Convert.ToInt32(Console.ReadLine());
                        Customers cusID = new Customers { Customer_id = cid };
                        repo.ViewCustomerOrders(cusID);
                        break;
                    case "10":
                        Console.Write("All Products using Entity Framework");
                        List<Products> pro = repo.ShowProducts();
                        Console.Write($"{"Product ID",-15} {"Name",-20} {"Price",10}");
                        foreach (var pr in pro)
                        {
                            Console.WriteLine($"ID: {pr.Product_Id}, Name: {pr.Name}, Price: ₹{pr.Price}");
                        }
                        break;
                    case "11":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Thank you for using the E-Commerce Appication");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;         
                }
            }
        }
    }
}
