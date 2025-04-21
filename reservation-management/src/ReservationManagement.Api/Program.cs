using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReservationManagement.Api;
using ReservationManagement.Api.Reservations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservationManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")).EnableSensitiveDataLogging());
builder.Services.TryAddScoped<DbContext>(sp => sp.GetRequiredService<ReservationManagementDbContext>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReservations();
builder.Services.AddScoped<ICodeProvider, GuidCodeProvider>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseReservationsApi();

app.Run();