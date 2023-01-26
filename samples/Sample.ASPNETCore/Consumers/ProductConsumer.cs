using GraphQL;
using GraphQL.Client.Abstractions;
using Sample.ASPNETCore.Models;

namespace Sample.ASPNETCore.Consumers;

public class ProductConsumer
{
    private readonly IGraphQLClient _client;
    public ProductConsumer(IGraphQLClient client)
    {
        _client = client;
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var query = new GraphQLRequest
        {
            Query = @"
                query products {
                  products(pageNumber: 1, pageSize : 10){
                    id,
                    name,
                    price,
                    description,
                    status,
                    productCategory{
                      id,
                      title
                    }
                  }
                }"
        };
        var response = await _client.SendQueryAsync<ResponseProductCollectionType>(query);
        return response.Data.Products;
    } 
    
    public async Task<ProductDTO> GetSingleProduct(int id)
    {
        var query = new GraphQLRequest
        {
            Query = @"query getSingleProduct($productId: ID!) {
                      product(id: $productId){
                        id,
                        name,
                        price,
                        description,
                        status,
                        productCategory{
                          title
                        }
                      }
                    }",
            Variables = new { productId = id }
        };
        var response = await _client.SendQueryAsync<ResponseProductType>(query);
        return response.Data.Product;
    }
}


public class ResponseProductCollectionType
{
    public List<ProductDTO> Products { get; set; } = new();
}

public class ResponseProductType
{
    public ProductDTO Product { get; set; } = new();
}