using System;

namespace api
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // The 9th field for Marcus Chen
        public string Condition { get; set; } = string.Empty;
    }
}
