using GraphQL;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Repositories;
using WebApi.GraphQL;
using WebApi.GraphQL.Queries;
using WebApi.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
        //.AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(p => p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConncetion")));

builder.Services.AddScoped<IAppDbContext>
    (p => p.GetRequiredService<AppDbContext>());

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<AppSchema>();
//builder.Services.AddScoped<ProductQuery>();
builder.Services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
builder.Services.AddSingleton<DataLoaderDocumentListener>();

builder.Services.AddGraphQL(p => p
    .AddSystemTextJson()
    .AddGraphTypes()
    .AddDataLoader()
    .AddUserContextBuilder(context=> new MyGraphQLUserContext(context.User))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseGraphQL<AppSchema>(); 
app.UseGraphQLPlayground();
app.Run();
