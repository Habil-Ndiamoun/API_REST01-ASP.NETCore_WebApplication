using Gestion_Livres.Data;
using Gestion_Livres.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

// Add the LivreContext
builder.Services.AddSqlite<LivreContext>("Data Source=BibliothequeLivres.db");

// Add the PizzaService and the ExemplaireService
builder.Services.AddScoped<LivreService>();
builder.Services.AddScoped<ExemplaireService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add the CreateDbIfNotExists method call
app.CreateDbIfNotExists();

app.Run();
