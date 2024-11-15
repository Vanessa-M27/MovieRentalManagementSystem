using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.DependencyInjection;
using MRMS_BusinessService;
using MRMS_Data;
using System;
using System.IO;

namespace MRMS_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Manually set the path to appsettings.json
                var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                // Debugging log: print the path
                Console.WriteLine("Attempting to load appsettings.json from: " + appSettingsPath);

                // Explicitly load the appsettings.json file
                builder.Configuration.AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);

                // Add services to the container
                builder.Services.AddControllers();

                // Register AWS service
                builder.Services.AddAWSService<IAmazonS3>();
                builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

                // Register business logic and data access classes
                builder.Services.AddScoped<CustomerGetService>();
                builder.Services.AddScoped<MovieService>();
                builder.Services.AddScoped<SqlCustomerdbData>();
                builder.Services.AddScoped<Email>();

                // Add Swagger
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error during startup: {ex.Message}");
                throw;
            }
        }
    }
}

