namespace EnergyConsumptionMonitor.API.ViewModels
{
    internal class MeterReadingUploadResults
    {
        public MeterReadingUploadResults(
            int meterReadingsSuccessfullyProcessed,
            int meterReadingsFailedToProcessed)
        {
            this.MeterReadingsSuccessfullyProcessed = meterReadingsSuccessfullyProcessed;
            this.MeterReadingsFailedToProcessed = meterReadingsFailedToProcessed;
        }

        public int MeterReadingsFailedToProcessed { get; }

        public int MeterReadingsSuccessfullyProcessed { get; }
    }
}