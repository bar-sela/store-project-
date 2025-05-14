using System;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/*
The code defines a class named DBInitializer, which is responsible for initializing and preparing the database for
 the application.
  This includes migrating the database schema and seeding initial data using Entity Framework Core.
*/

public class DBinitlaizer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
        
        try
        {
            Console.WriteLine("Starting database initialization...");
            Console.WriteLine("Database tables ensured or created.");

            context.Database.Migrate(); //> like dotnet ef database update
            Console.WriteLine("Database migration completed.");

            if (!context.Product.Any())
            {
                Console.WriteLine("No products found. Seeding initial data...");

                var products = new List<Product>
                {
                    new()
                    {
                        Name = "שון",
                        Description = "בוט",
                        Price = 2999,
                        Type = "צ'לסי",
                        Brand = "יונייטד",
                        PictureUrl = "boot-ang1.png",
                        QuantityInStock = 100
                    },
                        new()
                    {
                        Name = "איתי",
                        Description = "שכטר",
                        Price = 2999,
                        Type = "צ'לסי",
                        Brand = "יונייטד",
                        PictureUrl = "boot-ang2.png",
                        QuantityInStock = 100
                    }

                };

                context.Product.AddRange(products); 
                /*
                🔹 Entity Framework Core מנהל רשימה של אובייקטים שעברו שינוי אבל עדיין לא נכתבו למסד הנתונים.
                🔹 הרשימה הזאת נקראת Change Tracker והיא עוקבת אחרי האובייקטים שנוספו, עודכנו או נמחקו.
 

                */

                Console.WriteLine($"Adding {products.Count} product(s) to the database...");

                context.SaveChanges();
                Console.WriteLine($"Successfully added {products.Count} product(s) to the database.");
            }
            else
            {
                Console.WriteLine("Products already exist in the database. Skipping seeding.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error initializing database: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}
