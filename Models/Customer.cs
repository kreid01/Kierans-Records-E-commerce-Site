using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.Models
{
    public class Customer
    {
        [BsonId]
        [BsonElement("Id")]
        [Required]
        public string Id { get; set; }

        [BsonElement("LinkToken")]
        [Required]
        public string LinkToken { get; set; }

        [BsonElement("FirstNmae")]
        [Required]
        public string FirstName { get; set; }

        [BsonElement("SecondName")]
        [Required]
        public string SecondName { get; set; }

        [BsonElement("Email")]
        [Required]
        public string Email { get; set; }
        
        [BsonElement("PhoneNumber")]
        public int? PhoneNumber { get; set; }
        [Required]
        public string AddressFirstLine { get; set; }

        [BsonElement("AddressSecondLine")]
        [Required]
        public string AddressSecondLine { get; set; }

        [BsonElement("Postcode")]
        [Required]
        public string Postcode { get; set; }

    }
}
