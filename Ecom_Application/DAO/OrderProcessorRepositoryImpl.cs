using Ecom_Application.Entity;
using Ecom_Application.myexceptions;
using Ecom_Application.util;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;




namespace Ecom_Application.DAO
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {
        SqlConnection con = DBConnection.GetConnection();
        // Create_product
        public bool CreateProduct(Products product)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from products where name=@name ", con);
                cmd.Parameters.AddWithValue("@name", product.Name);
                object result = cmd.ExecuteScalar();
                int count = (result != null) ? Convert.ToInt32(result) : 0;
                if (count > 0)
                {
                    Console.WriteLine("Product Name - Already Exists");
                    return false;
                }
                else
                {
                    SqlCommand insert_cmd = new SqlCommand("insert into products (name,price,description,stockquantity) values (@name,@price,@description,@stockquantity)", con);
                    insert_cmd.Parameters.AddWithValue("@name", product.Name);
                    insert_cmd.Parameters.AddWithValue("@price", product.Price);
                    insert_cmd.Parameters.AddWithValue("@description", product.Description);
                    insert_cmd.Parameters.AddWithValue("@stockquantity", product.StockQuantity);
                    insert_cmd.ExecuteNonQuery();
                    Console.WriteLine("Product Name Created Successfully");
                    return true;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        // To Create_customer
        public bool CreateCustomer(Customers customer)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from customers where email=@email", con);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                object result = cmd.ExecuteScalar();
                int count = (result != null) ? Convert.ToInt32(result) : 0;
                if (count > 0)
                {
                    Console.WriteLine("Customer MailID - Already Exists");
                    return false;
                }
                else
                {
                    SqlCommand create_cmd = new SqlCommand("insert into customers (name, email, password) values (@name,@email,@password)", con);
                    create_cmd.Parameters.AddWithValue("@name", customer.Name);
                    create_cmd.Parameters.AddWithValue("@email", customer.Email);
                    create_cmd.Parameters.AddWithValue("@password", customer.Password);
                    create_cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer Created Successfully");
                    return true;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        // To Delete_product
        public bool DeleteProduct(int productid)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from products where Product_Id=@productid", con);
                cmd.Parameters.AddWithValue("@productid", productid);
                object result = cmd.ExecuteScalar();
                int count = result != null ? Convert.ToInt32(result) : 0;
                if (count == 0)
                {
       
                    throw new My_Exceptions.ProductNotFoundException(" Product not found.");
                }
                else
                {
                    SqlCommand delete_cmd = new SqlCommand("delete from products where Product_id=@productid", con);
                    delete_cmd.Parameters.AddWithValue("@productid", productid);
                    delete_cmd.ExecuteNonQuery();
                    Console.WriteLine("Product Deleted Sucessfully");
                    return true;
                }
            }
            finally
            {
                con.Close();
            }
        }

        // to Delete_Customer
        public bool DeleteCustomer(int customerid)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from customers where Customer_id=@customerid", con);
                cmd.Parameters.AddWithValue("@customerid", customerid);
                object result = cmd.ExecuteScalar();
                int count = (result != null) ? Convert.ToInt32(result) : 0;

                if (count == 0)
                {
                    Console.WriteLine("Customer - Not Found");
                    return false;
                }
                else
                {
                    SqlCommand delete_cmd = new SqlCommand("delete  from customers where customerid=@customerid", con);
                    delete_cmd.Parameters.AddWithValue("customerid", customerid);
                    delete_cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer- Deleted Sucessfully");
                    return true;
                }
            }
            finally
            {
                con.Close();
            }
        }
        // to Add_to_cart
        public bool AddToCart(Customers customer, Products product, int quantity)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from cart where customer_id=@customerid and product_id=@productid", con);
                cmd.Parameters.AddWithValue("@customerid", customer.Customer_id);
                cmd.Parameters.AddWithValue("@productid", product.Product_Id);
                object result = cmd.ExecuteScalar();
                int count = (result != null) ? Convert.ToInt32(result) : 0;
                if (count > 0)
                {
                    Console.WriteLine("Product - Already Exists");
                    return false;
                }
                else
                {
                    SqlCommand add_cmd = new SqlCommand(" insert into cart (customer_id,product_id) values (@customerid,@productid)", con);
                    add_cmd.Parameters.AddWithValue("@customerid", customer.Customer_id);
                    add_cmd.Parameters.AddWithValue("@productid", product.Product_Id);
                    add_cmd.Parameters.AddWithValue("@quantity", quantity);
                    add_cmd.ExecuteNonQuery();
                    Console.WriteLine(" Product Added Successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        // to Remove_from_cart
        public bool RemoveFromCart(Customers customer, Products product)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from cart where customer_id=@customerid and product_id=@productid", con);
                cmd.Parameters.AddWithValue("@customerid", customer.Customer_id);
                cmd.Parameters.AddWithValue("@productid", product.Product_Id);
                object result = cmd.ExecuteScalar();
                int count = (result != null) ? Convert.ToInt32(result) : 0;
                if (count == 0)
                {
                    Console.WriteLine("Product Not Exist ");
                    return false;
                }
                else
                {
                    SqlCommand remove_cmd = new SqlCommand("delete from cart where customer_id=@customerid and product_id=@productid", con);
                    remove_cmd.Parameters.AddWithValue("@customerid", customer.Customer_id);
                    remove_cmd.Parameters.AddWithValue("@productid", product.Product_Id);
                    remove_cmd.ExecuteNonQuery();
                    Console.WriteLine("Product Removed From Cart");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        // to  get all from cart - view cart
        public List<Products> GetAllFromCart(Customers customer)
        {
            List<Products> cartProducts = new List<Products>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT p.product_id, p.name, p.price, p.description, p.stockquantity
            FROM cart c
            JOIN products p ON c.product_id = p.product_id
            WHERE c.customer_id = @customerid", con);

                cmd.Parameters.AddWithValue("@customerid", customer.Customer_id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products product = new Products()
                    {
                        Product_Id = reader["product_id"] != DBNull.Value ? Convert.ToInt32(reader["product_id"]) : 0,
                        Name = reader["name"] != DBNull.Value ? reader["name"].ToString() : "No Name",
                        Price = reader["price"] != DBNull.Value ? (float)Convert.ToDecimal(reader["price"]) : 0,
                        Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : "No Description",
                        StockQuantity = reader["stockquantity"] != DBNull.Value ? Convert.ToInt32(reader["stockquantity"]) : 0
                    };
                    cartProducts.Add(product);
                    Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-10} {"Stock",-10}");
                    Console.WriteLine($"{product.Product_Id,-5} {product.Name,-20} {product.Price,-10} {product.StockQuantity,-10}  ");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return cartProducts;
        }
        // to print all the products 
        public void PrintAllProducts()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM products", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("No products available.");
                    return;
                }

                Console.WriteLine("\n-- PRODUCT LIST --");
                Console.WriteLine("ID\tName\t\tPrice\tStock");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Product_Id"],-5} {reader["name"],-15} {reader["price"],-10} {reader["stockquantity"],-5}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        // to place order
        public bool PlaceOrder(Customers customer, List<(Products products, int quantity)> items, string shippingAddress)
        {
            int orderId = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
            "INSERT INTO orders (customer_id, Order_Date, Shipping_Address) " +
            "OUTPUT INSERTED.Order_id " +
            "VALUES (@customerID, @OrderDate, @ShippingAddress)", con);
                cmd.Parameters.AddWithValue("@customerID", customer.Customer_id);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    Console.WriteLine("Order ID generation failed.");
                    return false;
                }
                orderId = Convert.ToInt32(result);

                foreach (var item in items)
                {
                    Products products = item.products;
                    int quantity = item.quantity;
                    if (products.StockQuantity < quantity)
                    {
                        Console.WriteLine("Not enough stock for product");
                        return false;
                    }
                    SqlCommand insert_cmd = new SqlCommand("INSERT INTO order_items (Order_Id, Product_id, quantity) VALUES (@OrderId, @product_id, @quantity)", con);
                    insert_cmd.Parameters.AddWithValue("@OrderId",orderId);
                    insert_cmd.Parameters.AddWithValue("@Product_id", products.Product_Id);
                    insert_cmd.Parameters.AddWithValue("@quantity", quantity);
                    insert_cmd.ExecuteNonQuery();

                    SqlCommand update_cmd = new SqlCommand("update products set stockquantity=stockquantity-@quan where product_id=@product_id", con);
                    update_cmd.Parameters.AddWithValue("@quan", quantity);
                    update_cmd.Parameters.AddWithValue("@product_id", products.Product_Id);
                    update_cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error placing order: "+ ex.Message);
                return false;
            }
            finally 
            {
                con.Close();
            }
        }
        // to view customer order
        public void ViewCustomerOrders(Customers customer)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@" SELECT o.Order_id, o.Order_Date, o.Shipping_address,p.name AS ProductName, oi.quantity FROM orders o  JOIN Order_Items oi ON o.Order_id = oi.Order_id  JOIN products p ON oi.Product_id = p.Product_Id WHERE o.Customer_id = @customerId  ORDER BY o.Order_id", con);

                cmd.Parameters.AddWithValue("@customerId", customer.Customer_id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {

                    throw new My_Exceptions.OrderNotFoundExcepetion("order not found");
                }

                Console.WriteLine($"\nOrders for Customer ID: {customer.Customer_id}");
                Console.WriteLine($"{"OrderID",-8} {"Date",-12} {"Product",-20} {"Qty",-5} {"Shipping Address",-30}");
                Console.WriteLine(new string('-', 85));

                while (reader.Read())
                {
                    int orderId = Convert.ToInt32(reader["Order_Id"]);
                    string orderDate = Convert.ToDateTime(reader["Order_Date"]).ToShortDateString();
                    string product = reader["ProductName"].ToString();
                    int quantity = Convert.ToInt32(reader["quantity"]);
                    string address = reader["Shipping_address"].ToString();

                    Console.WriteLine($"{orderId,-8} {orderDate,-12} {product,-20} {quantity,-5} {address,-30}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing orders: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        // to login 
        public Customers Login(string email, string password)
        {
            Customers customer = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE Email = @Email AND Password = @Password", con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customers
                    {
                        Customer_id = Convert.ToInt32(reader["Customer_id"]),
                        Name = reader["Name"].ToString(),
                       
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during login: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return customer;
        }
    //to show all the products using entity framework 
         public List<Products> ShowProducts()
         {
             using (var context = new ProductDbContext())
             {
                 return context.Products.ToList();
             }   
         }
    }
}



        



