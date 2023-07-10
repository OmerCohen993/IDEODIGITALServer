using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Server.Models;
using Server.Configurations;

namespace Server.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly IMongoCollection<Invoice> _invoiceCollection;

        public InvoicesService(IOptions<DatabaseSettings> mongoDBSettings) // Handle By Program.cs 
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUri);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DataBaseName);
            _invoiceCollection = database.GetCollection<Invoice>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Invoice>> GetIvoicesAsync() => await _invoiceCollection
        .Find(_ => true).ToListAsync();

        public async Task<Invoice> GetIvoiceAsync(string Id) => await _invoiceCollection.
        Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task EditInvoiceAsync(Invoice invoice) => await _invoiceCollection.
        ReplaceOneAsync(x => x.Id == invoice.Id, invoice);

        public async Task CreateIvoiceAsync(Invoice invoice) => await _invoiceCollection.
        InsertOneAsync(invoice);

        public async Task DeleteIvoiceAsync(string Id) => await _invoiceCollection.
        DeleteOneAsync(x => x.Id == Id);

    }
}