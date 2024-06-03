using BlogAPI.Data;
using BlogAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add controller
builder.Services.AddControllers();

//CROS
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

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
builder.Services.AddScoped<IRepository<Post>, Repository<Post>>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();
builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();



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
