namespace MenuApi.DTOs
{
    public class CreateOrUpdateMenuItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

        // Include category info (optional)
        public int CategoryId { get; set; }
        
    }
}
