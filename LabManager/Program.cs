using LabManager.Services;
using LabManager.Data;
using Microsoft.EntityFrameworkCore;


namespace LabManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
           
            builder.Services.AddSingleton<GreetingService>();

            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Angular", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            //DB SQL Server configuration

            builder.Services.AddDbContext<LabManagerDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            //DB SQL Server connection test

            using (IServiceScope scope = app.Services.CreateScope())
            {
                LabManagerDbContext dbContext =
                    scope.ServiceProvider.GetRequiredService<LabManagerDbContext>();

                bool canConnect = dbContext.Database.CanConnect();

                Console.WriteLine($"Database connection: {canConnect}");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {

                    options.SwaggerEndpoint("/openapi/v1.json", "Pagina LabManager");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("Angular");

            app.UseAuthorization();


            app.MapControllers();

           

            app.Run();
        }
    }
}
