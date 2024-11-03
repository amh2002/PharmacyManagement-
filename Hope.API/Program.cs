using Hope.EntityComponent.DBEntities;
using Hope.Repository;
using Hope.Repository.IRepository;
using Hope.Repository.Repository;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobDescriptionRepository, JobDescriptionRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineDepartmentRepository, MedicineDepartmentRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IModuleRoleRepository, ModuleRoleRepository>();
builder.Services.AddScoped<IAssignUsersToRoleRepository, AssignUsersToRoleRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
