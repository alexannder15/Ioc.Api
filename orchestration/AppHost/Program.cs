var builder = DistributedApplication.CreateBuilder(args);

// ToDo: Check why ioc tables are not created automatically when using ContainerLifetime.Persistent
var sql = builder.AddSqlServer("sql"); //.WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("ioc");

builder.AddProject<Projects.Api>("api").WithReference(db).WaitFor(db);

builder.Build().Run();
