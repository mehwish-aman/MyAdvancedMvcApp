var builder = WebApplication.CreateBuilder(args);

//Adding mvc services in our program
builder.Services.AddControllersWithViews();

var app = builder.Build();

//For static files
app.UseStaticFiles();

app.UseRouting();

//Default Mapping Controllers[ oper koi url specified
//na hon to default controller par chalay jao]
app.MapControllerRoute(
    name:"default", 
    pattern:"{Controller=Account}/{action=Login}/{id?}");
//app.MapGet("/", () => "Hi, This is My new App, I am working on it lets make something new!");

app.Run();
