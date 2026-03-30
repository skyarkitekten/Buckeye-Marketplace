using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        // For now, we use a static list to act as our "database" 
        // until we run the EF Migrations in the next step.
        private static List<CartItem> _cartItems = new List<CartItem>();

        [HttpGet]
        public ActionResult<Cart> GetCart()
        {
            return Ok(new Cart { Items = _cartItems });
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cartItems.Add(item);
            }
            return Ok();
        }

        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            _cartItems.Clear();
            return NoContent();
        }
    }
}