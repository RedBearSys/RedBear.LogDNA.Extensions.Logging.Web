using Newtonsoft.Json;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    public class WebMessageDetail : MessageDetail
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TraceId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IpAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserAgent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
    }
}
