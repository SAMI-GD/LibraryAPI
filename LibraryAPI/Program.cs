using LibraryAPI;
using LibraryAPI.DataAccess;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Profiles;
using LibraryAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ILibraryItemRepository, LibraryItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBorrowTransactionRepository, BorrowTransactionRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(LibraryItemsProfile));
builder.Services.AddAutoMapper(typeof(UsersProfile));
builder.Services.AddAutoMapper(typeof(BorrowTransactionProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });

    // Enable Swagger annotations
    c.EnableAnnotations();
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
