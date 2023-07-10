using Server.Models;

namespace Server.Services
{
    public interface IInvoicesService
    {
        Task<List<Invoice>> GetIvoicesAsync();
        Task<Invoice> GetIvoiceAsync(string Id);
        Task CreateIvoiceAsync(Invoice invoice);
        Task EditInvoiceAsync(Invoice invoice);
        Task DeleteIvoiceAsync(string Id);

    }
}