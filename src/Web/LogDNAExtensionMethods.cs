using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        public static LogDNAOptions AddWebItems(this LogDNAOptions options)
        {
            options.MessageDetailFactory.RegisterHandler(detail =>
            {
                var _contextAccessor = new HttpContextAccessor();

                detail.AddOrUpdateProperty("TraceId", _contextAccessor.HttpContext?.TraceIdentifier);
                detail.AddOrUpdateProperty("IpAddress",
                    _contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                detail.AddOrUpdateProperty("UserAgent",
                    _contextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString());
                detail.AddOrUpdateProperty("Language",
                    _contextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString().Split(',')
                        .FirstOrDefault());
                detail.AddOrUpdateProperty("Method", _contextAccessor.HttpContext?.Request.Method);
                detail.AddOrUpdateProperty("Url", _contextAccessor.HttpContext?.Request.GetDisplayUrl());
                detail.AddOrUpdateProperty("Identity", _contextAccessor.HttpContext?.User?.Identity?.Name);
            });

            return options;
        }
    }
}
