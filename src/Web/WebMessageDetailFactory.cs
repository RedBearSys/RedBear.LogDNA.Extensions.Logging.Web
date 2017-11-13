using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    public class WebMessageDetailFactory : IMessageDetailFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WebMessageDetailFactory(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public MessageDetail Create()
        {
            return new WebMessageDetail
            {
                TraceId = _contextAccessor.HttpContext?.TraceIdentifier,
                IpAddress = _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                UserAgent = _contextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                Language = _contextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault(),
                Method = _contextAccessor.HttpContext?.Request.Method,
                Url = _contextAccessor.HttpContext?.Request.GetDisplayUrl(),
                Identity = _contextAccessor.HttpContext?.User?.Identity?.Name
            };
        }
    }
}
