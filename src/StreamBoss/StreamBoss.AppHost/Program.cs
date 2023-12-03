var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");
var showApiService = builder.AddProject<Projects.StreamBoss_ShowApiService>("streamboss.ShowApiService");

var apiservice = builder.AddProject<Projects.StreamBoss_ApiService>("apiservice")
    .WithReference(showApiService);

builder.AddProject<Projects.StreamBoss_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiservice);


builder.Build().Run();
