# RedBear.LogDNA.Extensions.Logging.Web
[LogDNA](https://logdna.com) provider for [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging) in ASP.NET Core 2.0.

## Installation

```
Install-Package RedBear.LogDNA.Extensions.Logging.Web
```

## Configuring the provider

To add the LogDNA provider in **ASP.NET Core 2.0+**:

```csharp
public void ConfigureServices(IServiceCollection services)
  {
      services.AddLogging(loggingBuilder =>
      	loggingBuilder.AddLogDNAWeb("ingestion_key", LogLevel.Debug));
      
      // Other services ...
  }
```


## Optional AddLogDNAWeb()  parameters

The following optional parameters exist on `AddLogDNAWeb()`:

* `hostName` - used to override the machine's hostname. Defaults to `Environment.MachineName`;
* `tags` - to be associated with the host;
* `messageDetailFactory` - see next section;
* `inclusionRegex` - a case-sensitive regular expression that must be matched in order for log entries to be sent to LogDNA, e.g. `^MyWebApp\..+` .

```csharp
loggerfactory.AddLogDNA("ingestion_key", LogLevel.Debug, hostName: "myhost", tags: new [] { "tag1", "tag2" });
```

## MessageDetail class and IMessageDetailFactory

The `WebMessageDetail` class is serialised to create a JSON message for LogDNA to ingest:

```json
{
  message : "This is my messsage",
  level : "WARN",
  IpAddress : "127.0.0.1",
  Language : "en-GB",
  TraceId : "0HL99A22ATG42:00000001",
  Url : "http://localhost:62014/",
  UserAgent : "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36"
  Value : {
    Foo : "foo",
    Bar : "bar"
  }
}
```

Each new instance of a `WebMessageDetail` class is produced by an implementation of `IMessageDetailFactory`. The default implementation is `WebMessageDetailFactory`.

The contents of the `WebMessageDetail` class can be further customised by:

1. Creating a class that inherits from `WebMessageDetail`; and
2. Creating a new implementation of `IMessageDetailFactory` that inherits from `WebMessageDetailFactory`, and that creates instances of this class with any any additional properties populated.

## Enforcing JSON serialisation of objects

The Microsoft logging framework will occasionally try and use its own form of serialisation, typically when dealing with `IEnumerable<>` objects.

To ensure that the original object is serialised into JSON and that a value appears as follows in LogDNA, use the `Wrapper` class.

![An Array](docs/array.png)

```csharp
var array = new string[] { "one", "two", "three" };
logger.LogInformation("An array", new Wrapper(array));
```

The logger will *unwrap* the object prior to serialisation.

## Notes

Please remember that indexing of log entries only happens on paid accounts. This means you **won't** see JSON representations of objects or coloured highlighting of `INFO`, `WARN`, etc, if you are using a free account.

Also, please remember LogDNA's [standard service limits for ingestion](https://docs.logdna.com/docs/ingestion#section-service-limits).
