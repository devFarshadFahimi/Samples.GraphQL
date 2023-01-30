using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;
using WebApi.Data.Enums;

namespace WebApi.Data;

public class AppDbContextInitialiser
{
    private readonly ILogger<AppDbContextInitialiser> _logger;
    private readonly AppDbContext _context;

    public AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.ProductCategories.Any())
        {
            await _context.ProductCategories.AddRangeAsync(ProductCategories);
            await _context.SaveChangesAsync();
        }
        
        if (!_context.Products.Any())
        {
            var mobileCategoryId = await _context.ProductCategories
                .Where(p=>p.Title == "Mobile")
                .Select(p=>p.Id)
                .FirstAsync();

            var products = Products();
            foreach (var item in products)
            {
                item.CategoryId = mobileCategoryId;
            }

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }

    public IList<ProductCategory> ProductCategories => new List<ProductCategory>()
    {
        new ProductCategory()
        {
            Title= "Mobile",
        },
        new ProductCategory()
        {
            Title= "Clothes",
        },
    };

    public List<Product> Products() => new List<Product>()
    {
        new Product()
        {
            Name = "Samsung A10 Silver",
            Price = decimal.Parse("10550.382"),
            Status = EProductStatus.SoldOut,
            Description = "Description of Samsung A10 Silver"
        },
        new Product()
        {
            Name = "Samsung A70 gold",
            Price = decimal.Parse("22050.382"),
            Status = EProductStatus.SoldOut,
            Description = "Description of Samsung A70 gold"
        },
        new Product()
        {
            Name = "Samsung A12 Silver",
            Price = decimal.Parse("9550.382"),
            Status = EProductStatus.Available,
            Description = "Description of Samsung A12 Silver"
        },
        new Product()
        {
            Name = "Samsung A32",
            Price = decimal.Parse("16550.382"),
            Status = EProductStatus.Available,
            Description = "Description of Samsung A32"
        },
        new Product()
        {
            Name = "Samsung A30s",
            Price = decimal.Parse("1050.382"),
            Status = EProductStatus.SoldOut,
            Description = "Description of Samsung A30s"
        },
    };
}
