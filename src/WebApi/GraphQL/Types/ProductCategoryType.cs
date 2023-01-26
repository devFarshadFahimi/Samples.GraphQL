using GraphQL.DataLoader;
using GraphQL.Types;
using WebApi.Data.Entities;
using WebApi.Data.Repositories;

namespace WebApi.GraphQL.Types;

public class ProductCategoryType : ObjectGraphType<ProductCategory>
{
    public ProductCategoryType(IProductRepository productRepository, IDataLoaderContextAccessor dataLoader)
    {
        Field(p => p.Id);
        Field(p => p.Title);
        Field<ListGraphType<ProductType>, IEnumerable<Product>>("products")
            .ResolveAsync( p =>
            {
                var loader = dataLoader?.Context?.GetOrAddCollectionBatchLoader<int, Product>("GetAllProductsByCategoryIdWithLookUpAsync",
                    productRepository.GetAllProductsByCategoryIdWithLookUpAsync);
                return loader?.LoadAsync(p.Source.Id);
                //return productRepository.GetAllProductsByCategoryId(p.Source.Id);
            });
    }
}
