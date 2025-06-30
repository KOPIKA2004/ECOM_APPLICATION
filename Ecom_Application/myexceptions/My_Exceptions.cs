using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Application.myexceptions
{
    public class My_Exceptions
    {
        public class CustomerNotFoundExcepetion : Exception
        {
            public CustomerNotFoundExcepetion(string message) : base (message)  
            {
               Console.WriteLine(message); 
            }
            
        }
        public class ProductNotFoundException : Exception
        {
            public ProductNotFoundException(string message) : base(message)
            {
                Console.WriteLine(message);
            }

        }
        public class OrderNotFoundExcepetion : Exception
        {
            public OrderNotFoundExcepetion(string message) : base(message)
            {
                Console.WriteLine(message);
            }

        }

    }
}
