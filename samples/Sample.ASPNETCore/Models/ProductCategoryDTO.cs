namespace Sample.ASPNETCore.Models;

public class ProductCategoryDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public List<ProductDTO> Products { get; set; } = new();
}
