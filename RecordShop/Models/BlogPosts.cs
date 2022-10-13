using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace RecordShop.Models
{
    public class BlogPosts
    {

        public decimal price { get; set; }
    }
}
