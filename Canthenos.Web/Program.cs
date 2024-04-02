using Canthenos.Login;
using Canthenos.DataAccessLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<IPassword, Password>();

builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();

builder.Services.AddTransient<IGithubData, GithubData>();
builder.Services.AddTransient<ICookies, Cookies>();

builder.Services.AddTransient<IUsersData, UsersData>();
builder.Services.AddTransient<IMenusData, MenusData>();
builder.Services.AddTransient<IDrinksData, DrinksData>();
builder.Services.AddTransient<ISnacksData, SnacksData>();
builder.Services.AddTransient<IDishesData, DishesData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();