using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
         .WriteTo.File(
            path: "c:\\hotellisting\\logs\\log-.txt",
            outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLinw}{Exception}",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information
            )
         .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);


        builder.Host.UseSerilog();

        // Add services to the container.

        builder.Services.AddDbContext<DataBaseContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")
            ));
       
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage ();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        try
        {
            Log.Information("Application is Starting");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application Failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }
        
    }
}