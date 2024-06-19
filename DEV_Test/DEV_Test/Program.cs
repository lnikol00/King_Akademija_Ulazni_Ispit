
using DEV_Test.Models;
using DEV_Test.Services.AuthService;
using DEV_Test.Services.ProductService;
using DEV_Test.Services.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace DEV_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.Configure<ConnectionApi>(builder.Configuration.GetSection("ConnectionApi"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
