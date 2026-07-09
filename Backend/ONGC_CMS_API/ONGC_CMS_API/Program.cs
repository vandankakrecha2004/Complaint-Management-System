using Business;
using DataProvider.MySQL;
using DataProvider.MySQL.Config;
using IDataProvider.MySQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? CORS MUST BE BEFORE builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// DB Connection
var conStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<IiotIntelliscadaDbContext>(options =>
    options.UseMySql(conStr, ServerVersion.AutoDetect(conStr)));

// DI
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<ComplaintService>();

builder.Services.AddScoped<FtpService>();

// --------------------------------------------------


// apply strict json

var app = builder.Build();

// -------------------- PIPELINE --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? Use CORS AFTER Build
app.UseCors("AllowAngular");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();