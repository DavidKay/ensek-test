using EnergyConsumptionMonitor.Application.Contracts.Commands;
using EnergyConsumptionMonitor.Domain;
using EnergyConsumptionMonitor.Domain.Repositories;
using MediatR;

namespace EnergyConsumptionMonitor.Application.Services
{
    public class MeterReadingService :
        IRequestHandler<SupplyNewMeterReadingCommand, SupplyNewMeterReadingCommandResponse>
    {
        private readonly ICustomerAccountRepository customerAccountRepository;

        public MeterReadingService(ICustomerAccountRepository customerAccountRepository)
        {
            this.customerAccountRepository = customerAccountRepository;
        }

        public async Task<SupplyNewMeterReadingCommandResponse> Handle(SupplyNewMeterReadingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var suppliedMeterReading = new MeterReading(
                    request.AccountId,
                    request.TimeOfMeterReading,
                    request.ValueOfMeter);

                var customerAccount = await this.customerAccountRepository.GetCustomerAccount(request.AccountId);

                if (customerAccount == null)
                {
                    return SupplyNewMeterReadingCommandResponse.Failed($"Could not find an associated customer account for {request.AccountId}");
                }

                if (!customerAccount.AddMeterReadingToAccount(suppliedMeterReading))
                {
                    return SupplyNewMeterReadingCommandResponse.Failed($"There is already a current meter reading for accountId {suppliedMeterReading.AccountId}");
                }

                await this.customerAccountRepository.SaveChanges();
                return SupplyNewMeterReadingCommandResponse.Success();
            }
            catch (Exception exception)
            {
                return SupplyNewMeterReadingCommandResponse.Failed(exception.Message);
            }
        }
    }
}