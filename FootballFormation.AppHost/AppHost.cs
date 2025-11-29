using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db")
                .WithDataVolume()
                .AddDatabase("footballformationdb");

var api = builder.AddProject<FootballFormation_API>("api")
                 .WithReference(db)
                 .WaitFor(db);

builder.AddNpmApp("frontend", "../frontend", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithHttpEndpoint(env: "PORT", port: 5173)
       .WithExternalHttpEndpoints();

builder.Build().Run();