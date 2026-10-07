using Microsoft.Data.SqlClient;
using SupportApp.DB;
using SupportApp.Extensions;
using SupportApp.Mappers.Profiles;
using SupportApp.Repositories.Implementations;
using SupportApp.Repositories.Interfaces;
using SupportApp.Services;
using SupportApp.Services.Implementations;
using SupportApp.Services.Interfaces;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(sp =>
               new SqlConnection(builder.Configuration.GetConnectionString("MsSqlDb")));

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<ISupportRepository, SupportRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(x =>
{
    x.AddProfile<FileProfile>();
});

builder.Services.AddControllers();
builder.Services.AddSwaggerServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
