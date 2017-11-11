using Newtonsoft.Json;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    public class WebMessageDetail : MessageDetail
    {
        [JsonProperty("TraceId", NullValueHandling = NullValueHandling.Ignore)]
        public string TraceId { get; set; }

        [JsonProperty("IpAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string IpAddress { get; set; }

        [JsonProperty("UserAgent", NullValueHandling = NullValueHandling.Ignore)]
        public string UserAgent { get; set; }

        [JsonProperty("Language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}
