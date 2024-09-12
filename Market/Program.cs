
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Market.Abstractions;
using Market.Migrations;
using Market.Models;
using Market.Repo;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Path = System.IO.Path;

namespace Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Services.AddControllers();           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache(o => o.TrackStatistics = true);

            builder.Host.ConfigureContainer<ContainerBuilder>(conteinerBuilder =>
            {
                var configuration = builder.Configuration;
                string connectionString = configuration.GetConnectionString("db")!;
                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Пустая строка подключения");
                }

                conteinerBuilder.Register(c => new MarketContext(connectionString)).InstancePerDependency();
                conteinerBuilder.Register(c =>
                {
                    return new CategoryRepository(
                    c.Resolve<IMapper>(),
                    c.Resolve<IMemoryCache>(),
                    connectionString);
                }).As<ICategoryRepository>().InstancePerDependency();

                conteinerBuilder.Register(c =>
                {
                    return new StorageRepository(
                    c.Resolve<IMapper>(),
                    c.Resolve<IMemoryCache>(),
                    connectionString);
                }).As<IStorageRepository>().InstancePerDependency();

                conteinerBuilder.Register(c =>
                {
                    return new ProductRepository(
                    c.Resolve<IMapper>(),
                    c.Resolve<IMemoryCache>(),
                    connectionString);
                }).As<IProductRepository>().InstancePerDependency();                              
                
            } );
            
            var app = builder.Build();
                        
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            string staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
            Directory.CreateDirectory(staticFilePath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilePath),
                RequestPath = "/static"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
