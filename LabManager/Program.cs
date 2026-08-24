using LabManager.Contracts;
using LabManager.Data;
using LabManager.Services;
using LabManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using LabManager.Middleware;


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
            builder.Services.AddScoped<IExperimentsService, ExperimentsService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IEquipmentsService, EquipmentsService>();

            //DB SQL Server configuration

            builder.Services.AddDbContext<LabManagerDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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


           var app = builder.Build();

            // This middleware must appear early so it can catch
            // exceptions thrown by subsequent middleware and controllers.
            app.UseMiddleware<ExceptionHandlingMiddleware>();


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
