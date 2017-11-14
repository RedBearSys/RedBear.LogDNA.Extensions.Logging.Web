using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        #region "ILoggerFactory"
        public static async Task<ILoggerFactory> AddLogDNAWebAsync(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return await factory.AddLogDNAWebAsync(options);
        }

        public static async Task<ILoggerFactory> AddLogDNAWebAsync(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return await factory.AddLogDNAWebAsync(options);
        }

        public static async Task<ILoggerFactory> AddLogDNAWebAsync(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            var client = await SetUpClientAsync(options.IngestionKey, options.HostName, options.Tags);
            factory.AddProvider(new LogDNAProvider(client, options));
            return factory;
        }

        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return factory.AddLogDNAWebAsync(options).Result;
        }

        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return factory.AddLogDNAWebAsync(options).Result;
        }

        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            return factory.AddLogDNAWebAsync(options).Result;
        }
        #endregion

        #region "ILoggingBuilder"
        public static async Task<ILoggingBuilder> AddLogDNAWebAsync(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return await builder.AddLogDNAWebAsync(options);
        }

        public static async Task<ILoggingBuilder> AddLogDNAWebAsync(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return await builder.AddLogDNAWebAsync(options);
        }

        public static async Task<ILoggingBuilder> AddLogDNAWebAsync(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            var client = await SetUpClientAsync(options.IngestionKey, options.HostName, options.Tags);
            builder.AddProvider(new LogDNAProvider(client, options));
            return builder;
        }

        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return builder.AddLogDNAWebAsync(options).Result;
        }

        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            string ingestionKey,
            LogLevel logLevel)
        {
            var options = new LogDNAOptions(ingestionKey, logLevel)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return builder.AddLogDNAWebAsync(options).Result;
        }

        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            return builder.AddLogDNAWebAsync(options).Result;
        }
        #endregion

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
