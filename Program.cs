using notebook.Model;
using notebook.Config;
using notebook.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using notebook.Controllers.Secutiry;
using Microsoft.AspNetCore.Identity;
using BC = BCrypt.Net.BCrypt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); 
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService<User>, UserService>();
builder.Services.AddScoped<IRecordService<Record>, RecordService>();
builder.Services.AddScoped<DbContext, ConfigDB>();
builder.Services.AddDbContext<ConfigDB>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher>();    
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,
 
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
 
                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();    // аутентификация
app.UseAuthorization();     // авторизация
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader()
                            .AllowAnyMethod());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.Run();
