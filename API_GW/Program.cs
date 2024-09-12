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

            // ��������� Swagger ��� API Gateway
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ��������� ������������ Ocelot �� ����� ocelot.json
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("ocelot.json")
                .Build();

            // ������������ Ocelot
            builder.Services.AddOcelot(configuration);

            // ������������ Swagger ��� Ocelot
            builder.Services.AddSwaggerForOcelot(configuration);

            var app = builder.Build();

            // �������� Swagger ����� Ocelot
            app.UseSwagger();

            // ����������� Swagger ��� Ocelot UI
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait();

            // ������ middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.Run();
        }
    }
}
