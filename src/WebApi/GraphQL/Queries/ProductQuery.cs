using GraphQL;
using GraphQL.Types;
using System.Linq.Expressions;
using WebApi.Data.Repositories;
using WebApi.GraphQL.Types;

namespace WebApi.GraphQL.Queries;

public class ProductQuery : ObjectGraphType
{
    public ProductQuery(IProductRepository productRepository)
    {
        Field<ListGraphType<ProductType>>(
          "products"
        )
        .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "pageNumber" })
        .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "pageSize" })
        .Resolve(p =>
        {
            //var userContext = p.UserContext as MyGraphQLUserContext;
            //if (!userContext.User.Identity.IsAuthenticated)
            //{
            //    p.Errors.Add(new ExecutionError("Invalid credential"));
            //    return null;
            //}
            var pageNumber = p.GetArgument<int>("pageNumber");
            var pageSize = p.GetArgument<int>("pageSize");
            return productRepository.GetAll(pageNumber, pageSize)
                .SelectProjectTo(p.SubFields?.Keys.ToList()!);
        });

        Field<ListGraphType<ProductCategoryType>>("productCategories")
            .Resolve(p => productRepository.GetAllProductCategories());

        Field<ProductType>("product")
           .Arguments(new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "id" })
           .Resolve(p =>
           {
               var id = p.GetArgument<int>("id");
               var item = productRepository.GetProductById(id);
               return item;
           });
    }
}

