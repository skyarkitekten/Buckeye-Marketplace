using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using api;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                ID = 1,
                Title = "Hibbler Dynamics Textbook",
                Description = "Used engineering dynamics textbook",
                Price = 50m,
                Category = "Books",
                SellerName = "Alice",
                PostedDate = DateTime.UtcNow.AddDays(-10),
                ImageUrl = "https://picsum.photos/200",
                // add Condition property? not in model so maybe we extend via description
            },
            new Product
            {
                ID = 2,
                Title = "TI-84 Calculator",
                Description = "Graphing calculator in like new condition",
                Price = 60m,
                Category = "Electronics",
                SellerName = "Bob",
                PostedDate = DateTime.UtcNow.AddDays(-8),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 3,
                Title = "27-inch Monitor",
                Description = "27-inch Full HD monitor, used - good",
                Price = 120m,
                Category = "Electronics",
                SellerName = "Carol",
                PostedDate = DateTime.UtcNow.AddDays(-5),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 4,
                Title = "Mechanical Keyboard",
                Description = "RGB mechanical keyboard, used - good",
                Price = 40m,
                Category = "Electronics",
                SellerName = "Dave",
                PostedDate = DateTime.UtcNow.AddDays(-12),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 5,
                Title = "3D Printer PLA Filament",
                Description = "1kg spool of PLA filament, like new",
                Price = 20m,
                Category = "Tools",
                SellerName = "Eve",
                PostedDate = DateTime.UtcNow.AddDays(-4),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 6,
                Title = "Raspberry Pi 4",
                Description = "Raspberry Pi 4 Model B 4GB, used - good",
                Price = 35m,
                Category = "Electronics",
                SellerName = "Frank",
                PostedDate = DateTime.UtcNow.AddDays(-2),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 7,
                Title = "Set of Precision Screwdrivers",
                Description = "Set of 10 precision screwdrivers, like new",
                Price = 15m,
                Category = "Tools",
                SellerName = "Grace",
                PostedDate = DateTime.UtcNow.AddDays(-6),
                ImageUrl = "https://picsum.photos/200",
            },
            new Product
            {
                ID = 8,
                Title = "USB-C Power Bank",
                Description = "10000mAh power bank, used - good",
                Price = 25m,
                Category = "Electronics",
                SellerName = "Heidi",
                PostedDate = DateTime.UtcNow.AddDays(-1),
                ImageUrl = "https://picsum.photos/200",
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
