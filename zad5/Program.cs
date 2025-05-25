using Microsoft.EntityFrameworkCore;
using zad5.Data;
using zad5.Models;
using zad5.Services;

namespace zad5;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<PrescriptionDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
        );
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}