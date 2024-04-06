using Bai3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDb>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");
app.UseSession();
var db = new MyDb();
db.Database.EnsureCreated();

var prd = new Products();
prd.Name = "Name1";
prd.Price = 145500;
prd.Quantity = 123;
prd.Status = true;

var prd1 = new Products();
prd1.Name = "Name2";
prd1.Price = 12000000;
prd1.Quantity = 253;
prd1.Status = true;

var prd12 = new Products();
prd12.Name = "Name3";
prd12.Price = 154000;
prd12.Quantity = 353;
prd12.Status = false;

var prd2 = new Products();
prd2.Name = "Name4";
prd2.Price = 522000;
prd2.Quantity = 254;
prd2.Status = true;

db.Products.AddRange(new Products[] { prd, prd1, prd12, prd2 });
db.SaveChanges();
app.Run();
