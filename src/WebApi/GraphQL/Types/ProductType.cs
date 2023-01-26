using GraphQL.Types;
using WebApi.Data.Entities;

namespace WebApi.GraphQL.Types;
public class ProductType : ObjectGraphType<Product>
{
    public ProductType()
    {
        Field(p => p.Id);
        Field(p => p.Name);
        Field(p => p.Price);
        Field(p => p.Description);
        Field<ProductStatusEnumType>("Status")
            .Description("Status of the product which could be available or sold out.");
        Field<ProductCategoryType>("ProductCategory");
    }
}
