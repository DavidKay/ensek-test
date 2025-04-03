using EnergyConsumptionMonitor.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnergyConsumptionMonitor.Data.Configurations
{
    public class MeterReadingConfiguration : IEntityTypeConfiguration<MeterReading>
    {
        public void Configure(EntityTypeBuilder<MeterReading> builder)
        {
            builder.Property(b => b.AccountId).IsRequired();
            builder.Property(b => b.TimeOfMeterReading).IsRequired();
            builder.Property(b => b.ValueOfMeter).IsRequired();
        }
    }
}