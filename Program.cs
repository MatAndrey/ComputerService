using Community.Microsoft.Extensions.Caching.PostgreSql;
using ComputerService.Data;
using ComputerService.Data.Repositories;
using ComputerService.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    
builder.Services.AddControllersWithViews()
    .AddViewLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "ru", "en" };
    options.SetDefaultCulture(supportedCultures[0])
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedPostgreSqlCache(setup =>
{
    setup.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
    
    setup.SchemaName = builder.Configuration.GetValue<string>("PostgresCache:SchemaName", "public");
    setup.TableName = builder.Configuration.GetValue<string>("PostgresCache:TableName", "cache");
    setup.DisableRemoveExpired = builder.Configuration.GetValue<bool>("PostgresCache:DisableRemoveExpired", false);
    setup.CreateInfrastructure = true;
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(48);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/login";
        options.LogoutPath = "/admin/logout";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(48);
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ISupportRepository, SupportRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
