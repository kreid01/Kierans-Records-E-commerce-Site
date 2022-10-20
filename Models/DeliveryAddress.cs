using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.Models
{
    public class DeliveryAddress
    {

        public string FirstName { get; set; }

     
        public string SecondName { get; set; }

      
        public string Email { get; set; }

     
        public int? PhoneNumber { get; set; }
        
        public string AddressFirstLine { get; set; }

       
        public string AddressSecondLine { get; set; }

        public string Postcode { get; set; }
    }
}
