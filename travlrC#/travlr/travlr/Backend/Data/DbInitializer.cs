
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using travlr.Backend.Models;
using travlr.Backend.Data;

public static class DbInitializer
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        Console.WriteLine("Database Provider: " + context.Database.ProviderName);
        Console.WriteLine("Connection String in DbContext: " + context.Database.GetDbConnection().ConnectionString);
        if (await context.Trips.AnyAsync()) return;

        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "trips.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed data file not found: {filePath}");
        }

        var jsonData = await File.ReadAllTextAsync(filePath);
        var trips = JsonSerializer.Deserialize<List<Trip>>(jsonData);

        if (trips != null)
        {
            await context.Trips.AddRangeAsync(trips);
            await context.SaveChangesAsync();
        }
    }
}
