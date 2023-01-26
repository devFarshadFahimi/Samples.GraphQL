namespace WebApi.Data.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}
