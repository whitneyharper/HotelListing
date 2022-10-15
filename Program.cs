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

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

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