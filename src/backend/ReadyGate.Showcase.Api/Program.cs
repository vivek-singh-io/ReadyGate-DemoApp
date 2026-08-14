using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Authorization;
using ReadyGate.Showcase.Api.Endpoints;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;
using ReadyGate.Showcase.Api.Infrastructure.Seed;
using ReadyGate.Showcase.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ShowcaseDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Showcase") ?? "Data Source=readygate-showcase.db"));
builder.Services.AddScoped<TicketQueryService>();
builder.Services.AddScoped<CsvExportService>();
builder.Services.AddScoped<AccessRequestQueryService>();
builder.Services.AddScoped<DemoPermissionService>();

var app = builder.Build();

app.UseExceptionHandler();

await app.Services.InitializeDatabaseAsync();

app.MapGet("/", () => Results.Ok(new
{
    application = "ReadyGate Showcase API",
    purpose = "Hackathon repository-analysis fixture",
    status = "ready"
}));
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapTicketEndpoints();
app.MapAccessRequestEndpoints();

app.Run();

public partial class Program;
