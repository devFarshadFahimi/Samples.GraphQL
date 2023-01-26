using GraphQL.DataLoader;
using GraphQL;
using GraphQL.Types;
using WebApi.GraphQL.Queries;

namespace WebApi.GraphQL;
public class AppSchema : Schema
{
	public AppSchema(IServiceProvider serviceProvider)
		:base(serviceProvider)
	{
		Query = serviceProvider.GetRequiredService<ProductQuery>();

        var listener = serviceProvider.GetRequiredService<DataLoaderDocumentListener>();

        var executer = new DocumentExecuter();
        var result = executer.ExecuteAsync(opts => {
	        opts.Listeners.Add(listener);
        }).ConfigureAwait(false);
    }
}
