using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnergyConsumptionMonitor.Data.Repositories
{
    public class CustomerAccountRepository : ICustomerAccountRepository
    {
        private readonly EnergyConsumptionMonitorContext energyConsumptionMonitorContext;

        public CustomerAccountRepository(EnergyConsumptionMonitorContext energyConsumptionMonitorContext)
        {
            ArgumentNullException.ThrowIfNull(energyConsumptionMonitorContext, nameof(energyConsumptionMonitorContext));

            this.energyConsumptionMonitorContext = energyConsumptionMonitorContext;
        }

        public async Task<CustomerAccount?> GetCustomerAccount(int accountId)
        {
            return await this.energyConsumptionMonitorContext.CustomerAccounts
                .Include(c => c.MeterReadings)
                .FirstOrDefaultAsync(c => c.AccountId == accountId);
        }

        public async Task SaveChanges()
        {
            await this.energyConsumptionMonitorContext.SaveChangesAsync();
        }
    }
}