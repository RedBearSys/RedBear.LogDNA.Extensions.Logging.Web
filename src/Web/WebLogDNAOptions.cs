using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    // ReSharper disable once InconsistentNaming
    public class WebLogDNAOptions : LogDNAOptions
    {
        public WebLogDNAOptions(string ingestionKey) : base(ingestionKey)
        {
            MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor());
        }

        public WebLogDNAOptions(string ingestionKey, LogLevel logLevel) : base(ingestionKey, logLevel)
        {
            MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor());
        }
    }
}
