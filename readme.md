# Features

* Integrate etcd configuration into microsoft.extensions.configuration.
* Automatically listen for changes and refresh configuration.



# Installation

https://www.nuget.org/packages/Etcd.Configuration/

```
PM> Install-Package Etcd.Configuration
```

# Add etcd configuration

**The key in the return result will not contain the prefix**

```
"etcd": {
  "hosts": [
    "http://localhost:2379"
  ],
  "prefixKeys": [
    "/namespace1/",
    "/namespace2/"
  ],
  "username": "",        // Default : Empty String
  "password": "",        // Default : Empty String
  "caCert": "",          // Default : Empty String
  "clientCert": "",      // Default : Empty String
  "clientKey": "",       // Default : Empty String
  "publicRootCa": false, // Default : false
}
```

# Web Application

[See sample for details](https://github.com/beckjin/Etcd.Configuration/samples/Etcd.Configuration.API)

```
public static IHostBuilder CreateHostBuilder(string[] args) =>
  Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
      webBuilder.ConfigureAppConfiguration(builder =>
      {
        builder.AddEtcd(builder.Build().GetSection("etcd")); // Just one line of code
      });
      webBuilder.UseStartup<Startup>();
    });
```

# Console Application

[See sample for details](https://github.com/beckjin/Etcd.Configuration/samples/Etcd.Configuration.Console)

```
private static IConfiguration GetConfiguration()
{
  var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json", optional: true);

  builder.AddEtcd(builder.Build().GetSection("etcd"));

  return builder.Build();
}
```