using CloudFileStorage.UI.Helpers;
using CloudFileStorage.UI.Mappings;
using CloudFileStorage.UI.Services;
using CloudFileStorage.UI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


// Auth servislerini ekle
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileShareService, FileShareService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

builder.Services.AddAutoMapper(typeof(AuthProfile));
builder.Services.AddScoped<ApiRequestHelper>();
builder.Services.AddScoped<FileRequestHelper>();

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

app.UseSession(); // Session middleware buraya
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
