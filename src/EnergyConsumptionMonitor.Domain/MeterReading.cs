namespace EnergyConsumptionMonitor.Domain
{
    public class MeterReading
    {
        private static readonly int MaximumMeterReading = 99999;

        public MeterReading(
            int accountId,
            DateTime timeOfMeterReading,
            int valueOfMeter)
        {
            var meterReadingExceptions = new List<Exception>();

            if (accountId <= 0)
            {
                meterReadingExceptions.Add(new ArgumentException("An AccountId must be provided for a meter reading."));
            }

            if (timeOfMeterReading == DateTime.MinValue)
            {
                meterReadingExceptions.Add(new ArgumentException("A valid time of the meter reading must be provided."));
            }

            if (valueOfMeter < 0 || valueOfMeter > MaximumMeterReading)
            {
                meterReadingExceptions.Add(new ArgumentException($"An valid value between 0 and {MaximumMeterReading} must be provided for a meter reading."));
            }

            if (meterReadingExceptions.Count > 0)
            {
                throw new AggregateException(meterReadingExceptions);
            }

            this.AccountId = accountId;
            this.TimeOfMeterReading = timeOfMeterReading;
            this.ValueOfMeter = valueOfMeter;
        }

        private MeterReading()
        {
        }

        public int AccountId { get; }

        public int MeterReadingId { get; init; }

        public DateTime TimeOfMeterReading { get; }

        public int ValueOfMeter { get; }

        public bool IsMoreRecentThan(MeterReading? existingMeterReading)
        {
            if (existingMeterReading == null)
            {
                return true;
            }

            if (this.TimeOfMeterReading > existingMeterReading.TimeOfMeterReading)
            {
                return true;
            }

            return false;
        }
    }
}