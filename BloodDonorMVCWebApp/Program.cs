using BloodDonorMVCWebApp.Data;
using BloodDonorMVCWebApp.Data.UnitOfWork;
using BloodDonorMVCWebApp.Mapping;
using BloodDonorMVCWebApp.Repositories.Implementations;
using BloodDonorMVCWebApp.Repositories.Interfaces;
using BloodDonorMVCWebApp.Services.Implementations;
using BloodDonorMVCWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



builder.Services.AddDbContext<BloodDonorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBloodDonorRepository, BloodDonorRepository>();
builder.Services.AddScoped<IBloodDonorService, BloodDonorService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BloodDonor}/{action=Index}/{id?}");

app.Run();
