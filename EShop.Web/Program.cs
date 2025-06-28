using EShop.Domain;
using EShop.Domain.Identity_Models;
using EShop.Repository;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Implementation;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// 2. Identity
builder.Services.AddDefaultIdentity<EShopApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. DI for Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IProductImportService, ProductImportService>();
builder.Services.AddHttpClient<IProductService, ProductService>();

// New enhanced services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<ISearchService, SearchService>();

// 4. Stripe Configuration
builder.Services.Configure<StripeSettings>(options =>
{
    options.PublishableKey = builder.Configuration["Stripe:PublishableKey"]
                             ?? Environment.GetEnvironmentVariable("STRIPE_PUBLISHABLE_KEY");

    options.SecretKey = builder.Configuration["Stripe:SecretKey"]
                        ?? Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");

    if (!string.IsNullOrEmpty(options.SecretKey))
    {
        Stripe.StripeConfiguration.ApiKey = options.SecretKey;
    }
    else
    {
        Console.WriteLine("Stripe Secret Key is not set. Payments will not work.");
    }
});

// 5. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// 6. Controllers & JSON
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// 7. Swagger
builder.Services.AddSwaggerGen();

// 8. Build App
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapControllers();

app.Run();
