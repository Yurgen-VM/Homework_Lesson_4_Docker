
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BatcheAPI.Abstractions;
using BatcheAPI.DB;
using BatcheAPI.Mutatin;
using BatcheAPI.Query;
using BatcheAPI.Repo;

namespace BatcheAPI
{
    public class Program
    {
        // https://localhost:7035/Batch/Batch/get_batch
        // https://localhost:7032/Batch/Batch/get_batch (Gateway)

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache(o => o.TrackStatistics = true);

            builder.Host.ConfigureContainer<ContainerBuilder>(sb => {

                sb.Register(c => new BatchContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });

            builder.Services.AddSingleton<IBatchRepository, BatchRepo>();
            builder.Services.AddSingleton<IProductRepository, ProductRepo>();
            builder.Services.AddSingleton<ISupplierRepository, SupplierRepo>();

            builder.Services.AddGraphQLServer()
                .AddMutationType<BatchMutation>()
                .AddQueryType<BatchQuery>();

            var app = builder.Build();
            
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();           

            app.MapControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 

            app.MapGraphQL();

            app.Run();
        }
    }
}
