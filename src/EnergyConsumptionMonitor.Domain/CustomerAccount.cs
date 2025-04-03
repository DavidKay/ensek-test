namespace EnergyConsumptionMonitor.Domain
{
    public class CustomerAccount
    {
        private readonly List<MeterReading> meterReadings;

        public CustomerAccount(
            int accountId,
            string firstName,
            string lastName)
        {
            var customerAccountExceptions = new List<Exception>();

            if (accountId <= 0)
            {
                customerAccountExceptions.Add(new ArgumentException("An AccountId must be provided for a customer account."));
            }

            if (customerAccountExceptions.Count > 0)
            {
                throw new AggregateException(customerAccountExceptions);
            }

            this.AccountId = accountId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.meterReadings = new List<MeterReading>();
        }

        private CustomerAccount()
        {
        }

        public int AccountId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public IReadOnlyCollection<MeterReading> MeterReadings => meterReadings;

        public bool AddMeterReadingToAccount(MeterReading suppliedMeterReading)
        {
            var duplicateReading = this.MeterReadings.Where(m =>
                       m.TimeOfMeterReading == suppliedMeterReading.TimeOfMeterReading
                    && m.ValueOfMeter == suppliedMeterReading.ValueOfMeter)
                 .FirstOrDefault();

            var mostRecentMeterReading = this.MeterReadings
                 .OrderByDescending(mr => mr.TimeOfMeterReading)
                 .FirstOrDefault();

            if (duplicateReading == null && suppliedMeterReading.IsMoreRecentThan(mostRecentMeterReading))
            {
                this.meterReadings.Add(suppliedMeterReading);
                return true;
            }

            return false;
        }
    }
}