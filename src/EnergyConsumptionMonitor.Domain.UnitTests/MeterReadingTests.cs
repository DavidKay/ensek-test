namespace EnergyConsumptionMonitor.Domain.UnitTests
{
    [TestClass]
    public sealed class MeterReadingTests
    {
        [TestMethod]
        public void A_meter_reading_is_valid_if_it_has_a_zero_value()
        {
            // Arrange
            int accountId = 1;
            DateTime timeOfMeterReading = new DateTime(2025, 1, 1);
            int valueOfMeterReading = 0;

            // Act

            // Assert
            Assert.IsNotNull(new MeterReading(accountId, timeOfMeterReading, valueOfMeterReading));
        }

        [TestMethod]
        public void A_meter_reading_must_have_a_valid_reading_date()
        {
            // Arrange
            int accountId = 1;
            DateTime timeOfMeterReading = new DateTime();
            int valueOfMeterReading = 1;

            // Act

            // Assert
            Assert.ThrowsException<AggregateException>(() => new MeterReading(accountId, timeOfMeterReading, valueOfMeterReading));
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(100000)]
        public void A_meter_reading_must_have_a_valid_value(int valueOfMeterReading)
        {
            // Arrange
            int accountId = 1;
            DateTime timeOfMeterReading = new DateTime(2025, 1, 1);

            // Act

            // Assert
            Assert.ThrowsException<AggregateException>(() => new MeterReading(accountId, timeOfMeterReading, valueOfMeterReading));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void A_meter_reading_must_have_an_accountId_provided(int accountId)
        {
            // Arrange
            DateTime timeOfMeterReading = new DateTime(2025, 1, 1);
            int valueOfMeterReading = 1;

            // Act

            // Assert
            Assert.ThrowsException<AggregateException>(() => new MeterReading(accountId, timeOfMeterReading, valueOfMeterReading));
        }
    }
}