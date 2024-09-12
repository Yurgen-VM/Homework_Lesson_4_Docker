//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

//namespace API_GW
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            builder.Services.AddEndpointsApiExplorer();

//            builder.Services.AddSwaggerGen();

//            IConfiguration configuration = new ConfigurationBuilder()
//                .AddJsonFile("ocelot.json")
//                .Build();

//            builder.Services.AddOcelot(configuration);

//            builder.Services.AddSwaggerForOcelot(configuration);            

//            var app = builder.Build();


//            app.UseSwaggerForOcelotUI(opt =>
//            {

//                opt.PathToSwaggerGenerator = "/swagger/docs";

//            }).UseOcelot().Wait();


//            app.UseSwagger();

//            app.UseHttpsRedirection();

//            app.UseAuthorization();

//            app.Run();
//        }
//    }
//}

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace API_GW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем Swagger для API Gateway
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Загружаем конфигурацию Ocelot из файла ocelot.json
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("ocelot.json")
                .Build();

            // Регистрируем Ocelot
            builder.Services.AddOcelot(configuration);

            // Регистрируем Swagger для Ocelot
            builder.Services.AddSwaggerForOcelot(configuration);

            var app = builder.Build();

            // Включаем Swagger перед Ocelot
            app.UseSwagger();

            // Настраиваем Swagger для Ocelot UI
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait();

            // Прочие middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();
        }
    }
}
