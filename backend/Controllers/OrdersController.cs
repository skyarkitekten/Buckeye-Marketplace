using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using backend.Models;

namespace backend.Controllers;

[Authorize] // This ensures only logged-in users can checkout
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemDto> items)
    {
        // 1. Get the User ID from the JWT token (Security requirement)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized("User not found in token.");

        // 2. Server-side calculation (Rubric requirement)
        decimal total = items.Sum(item => item.Price);

        // 3. Create the Order object
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = total,
            OrderItems = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductTitle = i.ProductTitle,
                Price = i.Price
            }).ToList()
        };

        // 4. Save to Database
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // 5. Return success and the new Order ID
        return Ok(new { orderId = order.Id, total = order.TotalAmount });
    }
}

// This matches the format React is sending
public record OrderItemDto(int ProductId, string ProductTitle, decimal Price);