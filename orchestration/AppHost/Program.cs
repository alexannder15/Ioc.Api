var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050))
    .WithDataVolume(isReadOnly: false);

var postgresdb = postgres.AddDatabase("ioc");

builder.AddProject<Projects.Api>("api").WithReference(postgresdb);

builder.Build().Run();
