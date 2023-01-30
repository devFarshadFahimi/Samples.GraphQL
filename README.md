# v1 on 01-03-2023

# GraphQL.Net Sample
  In this repository there is a sample of GraphQL.Net Implemented with .NET 7 in both Server and Client side.
  
# What you should know about the sample:
  1 - Sample just includes some of the main topics such as Query,Argument and Data Loader at this level, other topics will push in the future.
  2 - Main project which is the GraphQL.Net Server implementation, is in the "src/WebApi" directory.
  3 - Other projects Which includes : ASP.NET Web Application Client, Angular Client, Bechmark Console will put in the "samples" directory.
  
  NOTE: There is only one sample in the "Samples" directory which is an ASP.NET Web Application Client to communicate with GraphQL Server for testing purposes.
  
# GraphQL.Net configurations:
  1 - The starting step to working with GraphQL was to install its packages which listed in below ,in csproj fromat:
````
    <PackageReference Include="GraphQL" Version="7.2.2" />
    <PackageReference Include="GraphQL.DataLoader" Version="7.2.2" />
    <PackageReference Include="GraphQL.Server.Transports.AspNetCore" Version="7.2.0" />
    <PackageReference Include="GraphQL.Server.Ui.Playground" Version="7.2.0" />
````
  
  2 - Configuring Schema and DataLoader Context Accessor in Startup file
````
    // Services
    builder.Services.AddScoped<AppSchema>();
    builder.Services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
    builder.Services.AddSingleton<DataLoaderDocumentListener>();

    builder.Services.AddGraphQL(p => p
        .AddSystemTextJson()
        .AddGraphTypes()
        .AddDataLoader()
        .AddUserContextBuilder(context=> new MyGraphQLUserContext(context.User))
    );
    ...
    var app = builder.Build();
    ...
    // Middlewares
    app.UseGraphQL<AppSchema>(); 
    app.UseGraphQLPlayground();
    ...
    app.Run();
````

With this configuration two endpoints will reachable if you run it in your Localhost as below: <br />
1 : https://localhost:port/graphql // which is running GraphQL server <br />
2 : https://localhost:port/ui/playground // which is an interface to working with GraphQL server <br />
<hr >

# Other implementations

  Other implementations such as Schema,Queries and Types placed in "src/WebApi/GraphQL" directory.

  As I said before in my LinkedIn post about this repository, I highly recommended reading below blogs sereis to get most of the code implementations 
  mean while going forward parallally with this repository that implemented with latest versions and updates. <br />
  Code-Maze Blogs : https://code-maze.com/graphql-asp-net-core-tutorial/
  <hr />
  
  please feel free to clone and contribute, and ask or submit you oppinion about this repository. <br>
  Here is my LinkedIn page : https://www.linkedin.com/in/farshadfahimi5/
  
  // following updates or future descriptions will add in below.
  
  
