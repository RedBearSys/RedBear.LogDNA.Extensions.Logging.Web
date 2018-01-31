using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RedBear.LogDNA.Extensions.Logging.Web
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class LogDNAExtensionMethods
    {
        #region "ILoggerFactory"
        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return factory.AddLogDNAWeb(options);
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
            return factory.AddLogDNAWeb(options);
        }

        public static ILoggerFactory AddLogDNAWeb(
            this ILoggerFactory factory,
            LogDNAOptions options)
        {
            var client = SetUpClient(options.IngestionKey, options.HostName, options.Tags);
            factory.AddProvider(new LogDNAProvider(client, options));
            return factory;
        }
        #endregion

        #region "ILoggingBuilder"
        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            string ingestionKey)
        {
            var options = new LogDNAOptions(ingestionKey)
            {
                MessageDetailFactory = new WebMessageDetailFactory(new HttpContextAccessor())
            };
            return builder.AddLogDNAWeb(options);
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
            return builder.AddLogDNAWeb(options);
        }

        public static ILoggingBuilder AddLogDNAWeb(
            this ILoggingBuilder builder,
            LogDNAOptions options)
        {
            var client = SetUpClient(options.IngestionKey, options.HostName, options.Tags);
            builder.AddProvider(new LogDNAProvider(client, options));
            return builder;
        }
        #endregion

        private static IApiClient SetUpClient(
            string ingestionKey,
            string hostName,
            IEnumerable<string> tags)
        {
            if (tags == null)
                tags = new List<string>();

            var config = new ConfigurationManager(ingestionKey) { Tags = tags };

            if (!string.IsNullOrEmpty(hostName))
                config.HostName = hostName;

            var client = config.Initialise();

            client.Connect();

            return client;
        }
    }
}
