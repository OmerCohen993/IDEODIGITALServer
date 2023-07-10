using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Server.Helpers;

namespace Server.Models
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string invoiceId { get; set; } = null!;

        public string? CustomerName { get; set; }
        public string? DeliveryAddress { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreated { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateUpdated { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public StatusEnum Status { get; set; }

        public decimal? Total { get; set; }

        public decimal? Tax { get; set; }

        public decimal? NetTotal { get; set; }
    }
}