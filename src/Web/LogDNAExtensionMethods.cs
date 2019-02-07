using System;
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
                var contextAccessor = new HttpContextAccessor();

                detail.AddOrUpdateProperty("TraceId", contextAccessor.HttpContext?.TraceIdentifier);
                detail.AddOrUpdateProperty("IpAddress",
                    contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString());
                detail.AddOrUpdateProperty("UserAgent",
                    contextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString());
                detail.AddOrUpdateProperty("Language",
                    contextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString().Split(',')
                        .FirstOrDefault());
                detail.AddOrUpdateProperty("Method", contextAccessor.HttpContext?.Request.Method);

                try
                {
                    detail.AddOrUpdateProperty("Url", contextAccessor.HttpContext?.Request.GetDisplayUrl());
                }
                catch (NullReferenceException)
                {
                    // Getting a NullReferenceException from the internals of GetDisplayUrl().
                    // This might be something to do with the response having ended but processing
                    // is still occurring. Nothing we can control or avoid.
                }
                
                detail.AddOrUpdateProperty("Identity", contextAccessor.HttpContext?.User?.Identity?.Name);
            });

            return options;
        }
    }
}
