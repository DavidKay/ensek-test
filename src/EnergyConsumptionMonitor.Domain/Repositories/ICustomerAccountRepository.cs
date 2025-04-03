namespace EnergyConsumptionMonitor.Domain.Repositories
{
    public interface ICustomerAccountRepository
    {
        public Task<CustomerAccount?> GetCustomerAccount(int accountId);

        public Task SaveChanges();
    }
}