using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Required for [Authorize]
using backend.Models; 

namespace backend.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Using a static list for now as per your setup
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Title = "Hibbeler Dynamics Textbook", Description = "Used engineering dynamics textbook", Price = 50m, Category = "Textbooks", SellerName = "EngineeringSenior99", PostedDate = DateTime.UtcNow.AddDays(-10), ImageUrl = "https://picsum.photos/200", Condition = "Like New" },
            new Product { Id = 2, Title = "TI-84 Plus", Description = "Graphing calculator", Price = 60m, Category = "Electronics", SellerName = "BuckeyeTech", PostedDate = DateTime.UtcNow.AddDays(-8), ImageUrl = "https://picsum.photos/200", Condition = "Like New" },
            new Product { Id = 3, Title = "4K Monitor 27-inch", Description = "Ultra HD monitor", Price = 120m, Category = "Electronics", SellerName = "JulieW", PostedDate = DateTime.UtcNow.AddDays(-5), ImageUrl = "https://picsum.photos/200", Condition = "Used - Good" },
            new Product { Id = 4, Title = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 40m, Category = "Electronics", SellerName = "MikeEE", PostedDate = DateTime.UtcNow.AddDays(-12), ImageUrl = "https://picsum.photos/200", Condition = "Like New" },
            new Product { Id = 5, Title = "Arduino Starter Kit", Description = "Complete Arduino starter kit", Price = 45m, Category = "Tools", SellerName = "SarahDev", PostedDate = DateTime.UtcNow.AddDays(-4), ImageUrl = "https://picsum.photos/200", Condition = "New" },
            new Product { Id = 6, Title = "Digital Multimeter", Description = "Professional grade multimeter", Price = 35m, Category = "Tools", SellerName = "AlexBuilder", PostedDate = DateTime.UtcNow.AddDays(-2), ImageUrl = "https://picsum.photos/200", Condition = "Used - Good" },
            new Product { Id = 7, Title = "Breadboard Set", Description = "Prototyping components", Price = 25m, Category = "Tools", SellerName = "ChrisTools", PostedDate = DateTime.UtcNow.AddDays(-6), ImageUrl = "https://picsum.photos/200", Condition = "New" },
            new Product { Id = 8, Title = "USB-C Power Bank", Description = "High-capacity power bank", Price = 35m, Category = "Electronics", SellerName = "TaylorElectro", PostedDate = DateTime.UtcNow.AddDays(-1), ImageUrl = "https://picsum.photos/200", Condition = "Like New" }
        };

        // PUBLIC: Anyone can see products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // ADMIN ONLY: Add a product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> Create(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            product.PostedDate = DateTime.UtcNow;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // ADMIN ONLY: Edit a product
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1) return NotFound();

            updatedProduct.Id = id;
            _products[index] = updatedProduct;
            return NoContent();
        }

        // ADMIN ONLY: Delete a product
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    } 
}