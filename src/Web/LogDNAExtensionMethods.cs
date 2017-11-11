using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        public static async Task<ILoggerFactory> AddLogDNAWebAsync(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            if (messageDetailFactory == null)
                messageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor());

            var client = await SetUpClientAsync(ingestionKey, hostName, tags);
            factory.AddProvider(new LogDNAProvider(client, logLevel, messageDetailFactory, inclusionRegex));
            return factory;
        }

        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            return factory.AddLogDNAWebAsync(ingestionKey, logLevel, hostName, tags, messageDetailFactory, inclusionRegex).Result;
        }

        public static async Task<ILoggingBuilder> AddLogDNAWebAsync(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            if (messageDetailFactory == null)
                messageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor());

            var client = await SetUpClientAsync(ingestionKey, hostName, tags);
            builder.AddProvider(new LogDNAProvider(client, logLevel, messageDetailFactory, inclusionRegex));
            return builder;
        }

        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel,
            string hostName = null,
            IEnumerable<string> tags = null,
            IMessageDetailFactory messageDetailFactory = null,
            string inclusionRegex = "")
        {
            return builder.AddLogDNAWebAsync(ingestionKey, logLevel, hostName, tags, messageDetailFactory, inclusionRegex).Result;
        }

        private static async Task<ApiClient> SetUpClientAsync(
            string ingestionKey,
            string hostName,
            IEnumerable<string> tags)
        {
            if (tags == null)
                tags = new List<string>();

            var config = new Config(ingestionKey) { Tags = tags };

            if (!string.IsNullOrEmpty(hostName))
                config.HostName = hostName;

            var client = new ApiClient();

            await client.ConnectAsync(config);

            return client;
        }
    }
}
