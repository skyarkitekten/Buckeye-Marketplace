using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // Links this item to a Product in your database
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        // Required for M4: Hardcoded for now, will be real Auth in M5
        public string UserId { get; set; } = "default-user";
    }
}