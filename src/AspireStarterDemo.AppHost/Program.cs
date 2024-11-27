var builder = DistributedApplication.CreateBuilder(args);

// Add Redis
// https://learn.microsoft.com/en-us/dotnet/aspire/caching/stackexchange-redis-integration
var cache = builder.AddRedis("cache")
    .WithRedisCommander();

var apiService = builder.AddProject<Projects.AspireStarterDemo_ApiService>("apiservice")
    .WithReference(cache);

builder.AddProject<Projects.AspireStarterDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();