using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Data.Repositories;
using WebShop.Service.Dtos;
using WebShop.Service.Interceptors;
using WebShop.Service.Mappings;
using WebShop.Service.Services;
using WebShop.Service.Validations;

namespace Webshop.API
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

            builder.Services.AddDbContext<WebShopContext>(options =>
                options
                .UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"),
                  b => b.MigrationsAssembly(typeof(WebShopContext).Assembly.FullName))
                .AddInterceptors(new AdjustProductPriceInterceptor()));


            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });


            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<WebShopContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}

