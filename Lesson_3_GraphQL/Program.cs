using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lesson_3_GraphQL.Abstractions;
using Lesson_3_GraphQL.Models;
using Lesson_3_GraphQL.Mutatin;
using Lesson_3_GraphQL.Query;
using Lesson_3_GraphQL.Repo;
using Lesson_3_GraphQL.Services;
using Market.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Lesson_3_GraphQL
{
    public class Program
    {

        // https://localhost:7007/Storage/Category/get_category
        // https://localhost:7032/Storage/Category/get_category (Gateway)

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache(o => o.TrackStatistics = true);
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.Register(c => new MarketContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddSingleton<IProductRepository, ProductService>();
            builder.Services.AddSingleton<IStorageRepository, StorageService>();
            builder.Services.AddSingleton<ICategoryRepository, CategoryService>();
            builder.Services.AddSingleton<IStorehouseRepository, StorehouseService>();

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MySimpleQuery>()
                .AddMutationType<MySimpleMutation>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGraphQL();

            app.MapControllers();

            app.Run();
        }
    }
}
