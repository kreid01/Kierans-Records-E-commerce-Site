using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.Models
{
    public class Cart
    {
        [BsonId]
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("CartContents")]
        [Required]
        public List<Dictionary<string, string>> CartContents { get; set; }
    }
}
