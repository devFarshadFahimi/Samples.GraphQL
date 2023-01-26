using GraphQL.Types;
using WebApi.Data.Enums;

namespace WebApi.GraphQL.Types;

public class ProductStatusEnumType : EnumerationGraphType<EProductStatus>
{
    public ProductStatusEnumType()
    {
        Name = "Status";
        Description = "Enumeration for the account type object.";
    }
}