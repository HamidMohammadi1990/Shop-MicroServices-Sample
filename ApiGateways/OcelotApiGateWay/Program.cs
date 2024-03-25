using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOcelot()
    .AddCacheManager(x => x.WithDictionaryHandle());

var app = builder.Build();

await app.UseOcelot();

builder.Configuration.AddJsonFile($"ocelot.{app.Environment.EnvironmentName}.json", true, true);

app.MapGet("/", () => "Hello World!");

app.Run();