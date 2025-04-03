using CsvHelper;
using CsvHelper.Configuration;
using EnergyConsumptionMonitor.API.Records;
using EnergyConsumptionMonitor.API.ViewModels;
using EnergyConsumptionMonitor.Application.Contracts.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EnergyConsumptionMonitor.API.Controllers
{
    [ApiController]
    public class MeterReadingController : Controller
    {
        private readonly int maxFileSize = 8000;
        private readonly IMediator mediator;

        public MeterReadingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> MeterReadingUploads(IFormFile meterReadingFile)
        {
            if (meterReadingFile.Length <= 0
                || meterReadingFile.Length > this.maxFileSize
                || !meterReadingFile.FileName.EndsWith("csv"))
            {
                return BadRequest();
            }

            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await meterReadingFile.CopyToAsync(stream);
            }

            var meterReadingRecords = new List<MeterReadingRecord>();
            int recordsSuccessful = 0;
            int recordsFailed = 0;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = args => recordsFailed++,
                ReadingExceptionOccurred = args => { recordsFailed++; return false; },
                MissingFieldFound = args => recordsFailed++,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                AllowComments = true,
            };

            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    meterReadingRecords.AddRange(csv.GetRecords<MeterReadingRecord>());
                }
            }

            foreach (var record in meterReadingRecords)
            {
                var supplyNewMeterReadingCommand = new SupplyNewMeterReadingCommand(
                    record.AccountId,
                    record.MeterReadingDateTime,
                    record.MeterReadValue);

                var response = await mediator.Send(supplyNewMeterReadingCommand);

                if (response.IsSuccess)
                {
                    recordsSuccessful++;
                }
                else
                {
                    recordsFailed++;
                }
            }

            return Ok(new MeterReadingUploadResults(recordsSuccessful, recordsFailed));
        }
    }
}