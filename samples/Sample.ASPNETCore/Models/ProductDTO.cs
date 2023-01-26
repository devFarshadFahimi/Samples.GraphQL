namespace Sample.ASPNETCore.Models;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public ProductCategoryDTO ProductCategory { get; set; } = null!;

    public EProductStatus Status { get; set; }

}
