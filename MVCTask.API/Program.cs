using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using MVCTask.BL.Interfaces;
using MVCTask.BL.Services;
using MVCTask.DAL.ADO;
using MVCTask.DAL.Interfaces;
using MVCTask.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IDatabaseService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new DatabaseService(configuration);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddCors(options => {
    options.AddPolicy("AllowMvc", policy => {
        policy.WithOrigins("https://localhost:5001") // MVC app URL
              .AllowCredentials() // Allow cookies
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "AuthCookie";
        options.Cookie.Domain = "localhost"; // Omit port for subdomain sharing
        options.Cookie.SameSite = SameSiteMode.None; // Allow cross-origin
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use HTTPS
    });
// Share Data Protection keys between API and MVC
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\SharedKeys")) // Shared folder
    .SetApplicationName("MySharedApp"); // Exact same name






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowMvc");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
