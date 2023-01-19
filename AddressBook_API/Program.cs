using AddressBook_API.Data;
using AddressBook_API.Data.Repository;
using AddressBook_API.Interfaces;
using AddressBook_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register datacontext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddCors(options => options.AddPolicy(name: "AddressBookOrigin",
    policy => {
        //policy.WithOrigins("http://localhost:4200")
        //    .AllowAnyMethod()
        //    .AllowAnyHeader();
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));

// Service injection
builder.Services.AddScoped<IAddressBookService, AddressBookService>();

builder.Services.AddScoped<IAddressBookRepository, AddressBookRepository>();



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
