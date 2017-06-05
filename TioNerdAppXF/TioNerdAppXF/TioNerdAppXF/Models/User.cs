using Newtonsoft.Json;

namespace TioNerdAppXF.Models
{
    public class FacebookProfile
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public string Age { get; set; }
        public Picture Picture { get; set; }
        public string Id { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
