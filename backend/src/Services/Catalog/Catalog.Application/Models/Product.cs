namespace AmigurumiStore.Catalog.Application.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string YarnType { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Beginner";
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
