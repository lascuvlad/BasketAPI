using AutoMapper;
using BasketAPI.Helpers;
using BasketAPI.Models;
using BasketAPI.Services;
using BasketAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasketAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            MapperConfiguration mapperConfig = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
            builder.Services.AddSingleton(mapperConfig.CreateMapper());
            builder.Services.AddScoped<IBasketService, BasketService>();

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