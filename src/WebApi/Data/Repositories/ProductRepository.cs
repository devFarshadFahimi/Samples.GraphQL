using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;

namespace WebApi.Data.Repositories;

public interface IProductRepository
{
    IQueryable<Product> GetAll(int pageNumber, int pageSize);
    Task<IReadOnlyList<Product>> GetAllAsync();
    IReadOnlyList<ProductCategory> GetAllProductCategories();
    IReadOnlyList<Product> GetAllProductsByCategoryId(int categoryId);
    Task<ILookup<int, Product>> GetAllProductsByCategoryIdWithLookUpAsync(IEnumerable<int> categoryIds);
    Product? GetProductById(int id);
}
public class ProductRepository : IProductRepository
{
    private readonly IAppDbContext _context;

    public ProductRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();
    public IQueryable<Product> GetAll(int pageNumber,int pageSize)
        => _context.Products
            .Include(p=>p.ProductCategory)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

    public IReadOnlyList<ProductCategory> GetAllProductCategories()
    => _context.ProductCategories.ToList();
    
    public Product? GetProductById(int id)
    => _context.Products
        .Include(p => p.ProductCategory)
        .Where(p=>p.Id == id)
        .FirstOrDefault();

    public IReadOnlyList<Product> GetAllProductsByCategoryId(int categoryId)
        => _context.Products.Where(p=>p.CategoryId == categoryId).ToList();

    public async Task<ILookup<int, Product>> GetAllProductsByCategoryIdWithLookUpAsync(IEnumerable<int> categoryIds)
    {
        var products = await _context.Products.Where(p => categoryIds.Contains(p.CategoryId)).ToListAsync();
        var lookUpResult = products.ToLookup(x => x.CategoryId);
        return lookUpResult;
    }
}
