using System.Text;
using BlogAPI.Data;
using BlogAPI.Model;
using BlogAPI.Repository;
using BlogAPI.Services.SendMailService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
}
);

//add controller
builder.Services.AddControllers();



//CROS
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//tich hop Identity
builder.Services.AddIdentity<BlogUser, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

// add & config  authen
builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        // chi ra token phải cấu hình expire(thời gian hết hạn)
                        RequireExpirationTime = true,
                        //chỉ ra Key mà sẽ dùng trong token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                    };
                });
//truy cap IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    // options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    // options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
    // options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = false; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
    // options.SignIn.RequireConfirmedAccount = true;
});

// kết nối sql server
builder.Services.AddDbContext<BlogDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAPI"));
    });

//dang ky automapper 
builder.Services.AddAutoMapper(typeof(Program));

//dang ky UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//dang ky repository
builder.Services.AddScoped<IRepositoryGenernic<Post>, Repository<Post>>();
builder.Services.AddScoped<IRepositoryGenernic<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepositoryGenernic<Author>, Repository<Author>>();
builder.Services.AddScoped<IRepositoryGenernic<Contact>, Repository<Contact>>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//doc config va dang ky de inject class MailSettings
builder.Services.AddOptions();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

// dang ki dich vu mailservice
builder.Services.AddTransient<ISendMailService, SendMailService>();

//
/*-------------------------------------------------------------------*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
