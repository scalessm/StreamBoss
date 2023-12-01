var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");

var apiservice = builder.AddProject<Projects.StreamBoss_ApiService>("apiservice");

builder.AddProject<Projects.StreamBoss_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiservice);

builder.AddProject<Projects.StreamBoss_MovieApiService>("streamboss.movieapiservice");

builder.Build().Run();
