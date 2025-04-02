using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //WebApplicationBuilder object, responsible for הגדרת the application, adding services  
// (like Controllers, databases, etc.).


//builder.Services - זהו אוסף השירותים של האפליקציה (Dependency Injection Container)
builder.Services.AddControllers();  //מוסיף תמיכה ב-Controllers, כלומר מאפשר לאפליקציה לעבוד כממשק API (Web API) המבוסס על REST.
/*
' adds all the necessary services for working with Controllers. 
When you call this function, you're essentially configuring your application to use the MVC (Model-View-Controller) 
architecture or API, but only the controllers part.
*/




builder.Services.AddDbContext<StoreContext>(opt=> 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();
// builds the application =  all configurations, controllers, and middleware .

app.MapControllers();
/*
"Go and find all the controllers in my code."
"Automatically create routes based on what is defined in the controllers using [Route]."
"When someone sends a request to a specific route, direct it to the appropriate action in the corresponding controller."
*/
DBinitlaizer.InitDb(app);   

app.Run(); ///  server starts running and begins accepting HTTP requests
