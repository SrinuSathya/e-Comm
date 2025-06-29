using MenuApi.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MenuApi.DTOs
{
    public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }

    // Include category info (optional)
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; } // Flattened from Category navigation
}
}
