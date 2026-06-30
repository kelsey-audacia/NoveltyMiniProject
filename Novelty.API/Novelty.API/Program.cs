using Microsoft.EntityFrameworkCore;
using Novelty.DataAccess;
using Novelty.Services.Interfaces;
using Novelty.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Service
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services
    .AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DatabaseContext") ?? string.Empty,
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();      // add this
    app.UseSwaggerUI();    // add this
}
app.UseCors("AllowVueApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
