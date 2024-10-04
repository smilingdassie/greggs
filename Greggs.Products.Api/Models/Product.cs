namespace Greggs.Products.Api.Models;

 
    public class Product
    {
        public int Id { get; set; }  // Ensure there is a primary key
        public string Name { get; set; }
        public decimal PriceInPounds { get; set; }
    }
 