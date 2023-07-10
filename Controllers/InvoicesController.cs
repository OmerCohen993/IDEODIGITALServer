using Microsoft.AspNetCore.Mvc;
using Server.Helpers;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoicesService _invoicesService;

        public InvoicesController(IInvoicesService invoicesService) => _invoicesService = invoicesService;

        [HttpGet("invoice")]
        public async Task<IActionResult> GetInvoice([FromQuery] string id)
        {
            var invoice = await _invoicesService.GetIvoiceAsync(id);

            if (invoice is null)
                return NotFound("The invoice id not found!");

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var allInvoices = await _invoicesService.GetIvoicesAsync();

            if (allInvoices is null)
                return NotFound("There is no Ivoices");

            return Ok(allInvoices);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Invoice invoice)
        {
            invoice.DateCreated = DateTime.Now;
            invoice.DateUpdated = DateTime.Now;
            await _invoicesService.CreateIvoiceAsync(invoice);
            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Invoice invoiceUpdated)
        {
            _ = invoiceUpdated.Id ?? throw new ArgumentNullException(nameof(invoiceUpdated));
            var existInvoice = await _invoicesService.GetIvoiceAsync(invoiceUpdated.Id);
            var response = new ResponseType();
            if (existInvoice is null)
                return BadRequest("the invoice was not found");

            existInvoice.Id = invoiceUpdated.Id;
            existInvoice.CustomerName = invoiceUpdated?.CustomerName ?? existInvoice.CustomerName;
            existInvoice.DeliveryAddress = invoiceUpdated?.DeliveryAddress ?? existInvoice.DeliveryAddress;
            existInvoice.invoiceId = invoiceUpdated?.invoiceId ?? existInvoice.invoiceId;
            existInvoice.NetTotal = invoiceUpdated?.NetTotal ?? existInvoice.NetTotal;
            existInvoice.Tax = invoiceUpdated?.Tax ?? existInvoice.Tax;
            existInvoice.Total = invoiceUpdated?.Total ?? existInvoice.Total;
            existInvoice.DateUpdated = DateTime.Now;
            existInvoice.Status = invoiceUpdated?.Status ?? existInvoice.Status;

            await _invoicesService.EditInvoiceAsync(existInvoice);
            return CreatedAtAction(nameof(GetInvoice), new { id = existInvoice.Id }, existInvoice);
        }

        [HttpDelete]
        public async Task<ResponseType> Delete([FromQuery] string id)
        {
            var existInvoice = await _invoicesService.GetIvoiceAsync(id);
            if (existInvoice is null)
                return new ResponseType { Value = "bad request", Result = "Not found" };


            await _invoicesService.DeleteIvoiceAsync(id);

            existInvoice = await _invoicesService.GetIvoiceAsync(id);

            if (existInvoice is null)
                return new ResponseType { Value = "ok", Result = "pass" };

            return new ResponseType { Value = "bad request", Result = "Not found" };
        }

    }
}