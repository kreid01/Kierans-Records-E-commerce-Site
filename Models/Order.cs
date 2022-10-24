using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.Models
{
    public class Order
    {
        [BsonElement("Id")]
        [Required]
        public string Id { get; set; }

        [BsonElement("CustomerLinkToken")]
        public string? CustomerLinkToken  { get; set; }

        [BsonElement("OrderContents")]
        [Required]
        public List<Dictionary<string, string>> OrderContents { get; set; }

        [BsonElement("TimeOfOrder")]
        [Required]
        public string TimeOfOrder { get; set; }

        [BsonElement("DeliveryAddress")]
        [Required]
        public DeliveryAddress DeliveryAddress { get; set; }

        [BsonElement("IsShipped")]
        public bool IsShippied { get; set; }
    }
}
