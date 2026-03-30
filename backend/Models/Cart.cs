namespace backend.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        
        // Automatically calculates the price based on quantity
        public decimal TotalPrice => Items.Sum(item => (item.Product?.Price ?? 0) * item.Quantity);
        
        public int TotalItems => Items.Sum(item => item.Quantity);
    }
}