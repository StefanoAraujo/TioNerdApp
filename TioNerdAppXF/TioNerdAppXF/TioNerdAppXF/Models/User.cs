using Newtonsoft.Json;

namespace TioNerdAppXF.Models
{
    public class User
    {       
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
