using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LibraryAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AvailabilityStatus
    {
       
        Available,
        Borrowed,
        Reserved
    }
}
