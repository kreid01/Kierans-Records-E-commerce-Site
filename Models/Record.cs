using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace RecordShop.Models
{
    public class Record
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement]
        public int? StockNumber { get; set; }

        [BsonElement("name")]
        [Required]
        public string name { get; set; }

        [BsonElement("artist")]
        [Required]
        public string artist { get; set; }


        [BsonElement("releaseYear")]
        [Required]
        [Range(1940, 2030)]
        public int releaseYear { get; set; }

        [BsonElement("songCount")]
        [Required]
        [Range(1, 40)]
        public int songCount { get; set; }
       
        [BsonElement("imageUrl")]
        [Required]
        public string imageUrl { get; set; }

        [BsonElement("genres")]
        [Required]
        public List<string> genres { get; set; }

        [BsonElement("quantity")]
        [Required]
        public int quantity { get; set; }

        [BsonElement("price")]
        [Required]
        [Range(1, 200)]
        public decimal price { get; set; }

        [BsonElement("isAvailable")]
        public bool? isAvailable { get; set; } = true;

        [BsonElement("isReservedInCart")]
        public bool? isReservedInCart { get; set; } = false;
    }
}
