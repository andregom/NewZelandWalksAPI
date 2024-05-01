using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using UdmNewZelandWalksAPI.Database;
using UdmNewZelandWalksAPI.Repositories;
using UdmNewZelandWalksAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NZWalksDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "New Zeland Walks",
        Description = "An ASP.NET Core Web API for exploring New Zeland Walks",
        //TermsOfService = new Uri("https://example.com/terms
        License = new OpenApiLicense
        {
            Name = "Dell DFS",
            Url = new Uri("https://dell.com"),
        },
        Contact = new OpenApiContact
        {
            Name = "André Gomes",
            Email = "andreoiveira@dell.com"
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
