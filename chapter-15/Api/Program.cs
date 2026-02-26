using Api;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StacyClouds.SwaAuth.Models;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();
builder.Services.AddScoped<IRoleProcessor, RoleProcessor>();

builder.Build().Run();
