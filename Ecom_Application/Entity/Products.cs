using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Ecom_Application.Entity
{
    public class Products
    {
       
        [Key]public int Product_Id { get; set; }

        public string Name { get; set; }
        public double Price {  get; set; }
        public string Description { get; set; }
        public int StockQuantity {  get; set; }
       


    }
}
