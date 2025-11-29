using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var dbPassword = builder.AddParameter("dbPassword", "dev_password");

var db = builder.AddPostgres("db", password: dbPassword)
                .WithDataVolume()
                .WithEndpoint(port: 54321,targetPort: 5432, name: "external")
                .AddDatabase("footballformationdb");

builder.AddContainer("pgadmin", "dpage/pgadmin4")
       .WithHttpEndpoint(targetPort: 80, name: "http")
       .WithEnvironment("PGADMIN_DEFAULT_EMAIL", "admin@example.com")
       .WithEnvironment("PGADMIN_DEFAULT_PASSWORD", "admin")
       .WithReference(db);

var api = builder.AddProject<FootballFormation_API>("api")
                 .WithReference(db)
                 .WaitFor(db);

builder.AddNpmApp("frontend", "../frontend", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithHttpEndpoint(env: "PORT", port: 5173)
       .WithExternalHttpEndpoints();

builder.Build().Run();