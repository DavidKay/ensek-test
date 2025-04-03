using MediatR;

namespace EnergyConsumptionMonitor.Application.Contracts.Commands
{
    public class SupplyNewMeterReadingCommand : IRequest<SupplyNewMeterReadingCommandResponse>
    {
        public SupplyNewMeterReadingCommand(
                  int accountId,
                  DateTime timeOfMeterReading,
                  int valueOfMeter)
        {
            this.AccountId = accountId;
            this.TimeOfMeterReading = timeOfMeterReading;
            this.ValueOfMeter = valueOfMeter;
        }

        public int AccountId { get; }

        public DateTime TimeOfMeterReading { get; }

        public int ValueOfMeter { get; }
    }

    public class SupplyNewMeterReadingCommandResponse
    {
        private SupplyNewMeterReadingCommandResponse(
            bool isSuccess,
            string errorMessage)
        {
            this.IsSuccess = isSuccess;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool IsSuccess { get; }

        public static SupplyNewMeterReadingCommandResponse Failed(string errorMessage)
        {
            return new SupplyNewMeterReadingCommandResponse(false, errorMessage);
        }

        public static SupplyNewMeterReadingCommandResponse Success()
        {
            return new SupplyNewMeterReadingCommandResponse(true, string.Empty);
        }
    }
}