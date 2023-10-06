using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPI v1"));
app.UseSwagger(x => x.SerializeAsV2 = true);

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.MapGet("/employee/{id}", ([FromServices] IDataRepository repository, string id) =>
{
    return repository.GetEmployeeById(id);
});

app.MapGet("/employees", ([FromServices] IDataRepository repository) =>
{
    return repository.GetEmployees();
});

app.MapPost("/employee/create", ([FromServices] IDataRepository repository, Employee employee) =>
{
    return repository.AddEmployee(employee);
});

app.MapPut("/employee/alter", ([FromServices] IDataRepository repository, Employee employee) =>
{
    return repository.AlterEmployee(employee);
});

app.Run();

void SeedData(IHost app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    if (scopedFactory != null)
    {
        using (IServiceScope scope = scopedFactory.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<DataSeeder>();

            if (service != null)
                service.Seed();
        }
    }
}