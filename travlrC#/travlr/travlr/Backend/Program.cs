
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using Blazored.LocalStorage;
using travlr.Backend.Repositories;
using travlr.Backend.Service;
using travlr.Models;
using travlr.Backend.Data;
using Microsoft.AspNetCore.Hosting;
using travlr.Frontend;
using Microsoft.AspNetCore.Components.WebAssembly.Server;


public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                   // var builder = services.AddRazorPages();

                    // Database Connection
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new InvalidOperationException("Database connection string is missing.");
                    }

                    // Add DbContext
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    // Identity Setup
                    services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

                    // API Client
                    services.AddHttpClient("API", client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:7094/"); // Adjust API URL
                    });

                    // Register Services
                    services.AddScoped<ITravelService, TravelService>();
                    services.AddScoped<UserRepository>(); // Fix repository registration
                    services.AddScoped<AuthenticationService>();
                    services.AddHttpClient<TripService>();

                    // Blazor & Razor Pages
                    services.AddRazorPages();
                    services.AddServerSideBlazor();
                    services.AddBlazoredLocalStorage();
                    services.AddHttpClient<TripService>();

                    // Swagger
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();
                   





                    // Authentication & Authorization
                    var jwtSecret = context.Configuration["Jwt:Secret"] ?? "DefaultFallbackSecret";
                    var key = Encoding.UTF8.GetBytes(jwtSecret);

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
                   


                    services.AddAuthorization();
                })
                .Configure((context, app) =>
                {
                    var env = context.HostingEnvironment;  // Correctly access IWebHostEnvironment

                    // Database Migration & Seeding
                    using (var scope = app.ApplicationServices.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        DbInitializer.Seed(services).Wait();  // Use Wait to handle async calls
                        var hostingcontext = services.GetRequiredService<ApplicationDbContext>();
                        hostingcontext.Database.Migrate();
                    }

                    // Middleware
                    if (!env.IsDevelopment())  // Use env variable here instead of app.Environment
                    {
                        app.UseExceptionHandler("/Error");
                        app.UseHsts();
                    }
                    
                    app.UseHttpsRedirection();
                   
                    app.UseStaticFiles();
                    app.UseBlazorFrameworkFiles();

                    

                    app.UseRouting();

                    app.UseAuthentication(); // Ensure this is before Authorization
                    app.UseAuthorization();


                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapRazorPages();
                        endpoints.MapBlazorHub();  // This is correct for Blazor
                        endpoints.MapFallbackToPage("/_Host");  // Ensure this points to your Blazor _Host.cshtml
                        endpoints.MapRazorPages();  // Map Razor Pages routes if needed

                        foreach (var endpoint in endpoints.DataSources.SelectMany(ds => ds.Endpoints))
                        {
                            Console.WriteLine($"Registered Endpoint: {endpoint.DisplayName}");
                        }
                    });

                });
            });
}
