# Features

* Integrate etcd configuration into **IConfigurationBuilder**.
* Listen for etcd configuration changes and automatically refresh the configuration in the application when there are changes.



# Installation

https://www.nuget.org/packages/Etcd.Configuration/

```
PM> Install-Package Etcd.Configuration
```

# Add etcd configuration

**The key in the return result will not contain the prefix**

```
{
  "etcd": {
    "connectionString": "http://localhost:2379",
    "env": "/dev",
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
    "keyMode": 1           // Default : 1, such as "prefixKey:key"
  }
}
```

# Web Application

[See sample for details](https://github.com/beckjin/Etcd.Configuration/tree/master/samples/Etcd.Configuration.API)

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

[See sample for details](https://github.com/beckjin/Etcd.Configuration/tree/master/samples/Etcd.Configuration.Console)

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