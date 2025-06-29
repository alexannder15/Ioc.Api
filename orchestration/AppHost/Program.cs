var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql").WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("ioc");

builder.AddProject<Projects.Api>("api").WithReference(db).WaitFor(db);

builder.Build().Run();
