using System.ComponentModel.DataAnnotations;

namespace MenuApi.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

}
