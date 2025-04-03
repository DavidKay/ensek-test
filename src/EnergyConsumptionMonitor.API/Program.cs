using EnergyConsumptionMonitor.Data;
using EnergyConsumptionMonitor.Data.Repositories;
using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionMonitor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<EnergyConsumptionMonitorContext>(options =>
                options.UseInMemoryDatabase("EnergyConsumptionMonitorContext"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyHook).Assembly);
            });

            var app = builder.Build();

            // Seed data for in-memory database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EnergyConsumptionMonitorContext>();

                if (!context.CustomerAccounts.Any())
                {
                    context.CustomerAccounts.AddRange(
                        new CustomerAccount(1234, "Freya", "Test"),
                        new CustomerAccount(1239, "Noddy", "Test"),
                        new CustomerAccount(1240, "Archie", "Test"),
                        new CustomerAccount(1241, "Lara", "Test"),
                        new CustomerAccount(1242, "Tim", "Test"),
                        new CustomerAccount(1243, "Graham", "Test"),
                        new CustomerAccount(1244, "Tony", "Test"),
                        new CustomerAccount(1245, "Neville", "Test"),
                        new CustomerAccount(1246, "Jo", "Test"),
                        new CustomerAccount(1247, "Jim", "Test"),
                        new CustomerAccount(1248, "Pam", "Test"),
                        new CustomerAccount(2233, "Barry", "Test"),
                        new CustomerAccount(2344, "Tommy", "Test"),
                        new CustomerAccount(2345, "Jerry", "Test"),
                        new CustomerAccount(2346, "Ollie", "Test"),
                        new CustomerAccount(2347, "Tara", "Test"),
                        new CustomerAccount(2348, "Tammy", "Test"),
                        new CustomerAccount(2349, "Simon", "Test"),
                        new CustomerAccount(2350, "Colin", "Test"),
                        new CustomerAccount(2351, "Gladys", "Test"),
                        new CustomerAccount(2352, "Greg", "Test"),
                        new CustomerAccount(2353, "Tony", "Test"),
                        new CustomerAccount(2355, "Arthur", "Test"),
                        new CustomerAccount(2356, "Craig", "Test"),
                        new CustomerAccount(4534, "JOSH", "TEST"),
                        new CustomerAccount(6776, "Laura", "Test"),
                        new CustomerAccount(8766, "Sally", "Test"));
                    context.SaveChanges();
                }
            }

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
        }
    }
}