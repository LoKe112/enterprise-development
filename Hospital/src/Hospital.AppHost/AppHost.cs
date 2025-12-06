var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddMySql("db")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("HospitalDatabase");

builder.AddProject<Projects.Hospital_Api>("hospital-api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
