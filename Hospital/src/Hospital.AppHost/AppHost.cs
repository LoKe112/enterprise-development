var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddMySql("db");
    
var db = sql.AddDatabase("HospitalDatabase");

var api = builder.AddProject<Projects.Hospital_Api>("hospital-api")
    .WithReference(db)
    .WaitFor(db);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var rabbitMq = builder.AddRabbitMQ("RabbitMQ", username, password)
    .WithManagementPlugin();

var consumer = builder.AddProject<Projects.Hospital_RabbitMqConsumer>("RabbitMqConsumer")
    .WithReference(rabbitMq)
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.Hospital_RabbitMqProducer>("RabbitMqProducer")
    .WithReference(api)
    .WithReference(rabbitMq)
    .WaitFor(api)
    .WaitFor(rabbitMq)
    .WaitFor(consumer);
    
builder.Build().Run();
