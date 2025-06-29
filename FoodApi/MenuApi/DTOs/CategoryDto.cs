using MenuApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MenuApi.DTOs
{
    public class CategoryDto
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }

        public ICollection<MenuItemDto> MenuItem { get; set; }

    }
}
