using API.Contracts;
using API.Data;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OvertimeDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});

// Add Email Service to the container.
builder.Services.AddTransient<IEmailHandler, EmailHandler>(_ => new EmailHandler(
    builder.Configuration["SmtpService:Server"],
    int.Parse(builder.Configuration["SmtpService:Port"]),
    builder.Configuration["SmtpService:FromEmailAddress"]
    ));

// Add Token Handler to the container.
builder.Services.AddScoped<ITokenHandler, TokenHandler>();

// Add repositories to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IOvertimeRepository, OvertimeRepository>();
builder.Services.AddScoped<IPaymentDetailsRepository, PaymentDetailsRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
       {
           // Custom validation response
           options.InvalidModelStateResponseFactory = context =>
           {
               var errors = context.ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(v => v.ErrorMessage);

               return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
           };
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();