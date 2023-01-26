using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Data.Enums;

namespace WebApi.Data.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price{ get; set; }
    public string? Description { get; set; }

    public EProductStatus Status { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public ProductCategory ProductCategory { get; set; } = null!;
}
