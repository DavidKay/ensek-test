using CsvHelper.Configuration.Attributes;

namespace EnergyConsumptionMonitor.API.Records
{
    internal class MeterReadingRecord
    {
        public int AccountId { get; set; }

        [Format("dd/MM/yyyy hh:mm")]
        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }
    }
}