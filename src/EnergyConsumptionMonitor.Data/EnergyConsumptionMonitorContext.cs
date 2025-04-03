using EnergyConsumptionMonitor.Domain;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionMonitor.Data
{
    public class EnergyConsumptionMonitorContext : DbContext
    {
        public EnergyConsumptionMonitorContext(DbContextOptions<EnergyConsumptionMonitorContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; } = default!;

        public DbSet<MeterReading> MeterReadings { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnergyConsumptionMonitorContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}