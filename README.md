### File Server extensions of ASP.NET Core

An extensions of file server on ASP.NET core to support file version,
Because currently ASP.NET core can't add version to file from file server.

#### Get Started

##### Register Tags Helper
Views/_ViewImports.cshtml

```csharp
// _ViewImports.cshtml
@using FileServerPlus.Mvc.Extensions
... 
@addTagHelper *, FileServerPlus.Mvc
```

##### Add configuration

```csharp
// Startup.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    var rootDirectory1 = new DirectoryInfo("..\\storages\\storage1\\");
    app.UseFileServerPlus("Server1", rootDirectory1, "/storage1", enableDirectoryBrowsing: true);
    ...
}
```

or from appsettings

```json
// appsettings.json
{
  "FileServers": [
    {
      "ServerId": "Server1",
      "RootDirectory": "..\\storages\\storage1\\",
      "RequestPath": "/storage1",
      "EnableDirectoryBrowsing": false
    }
  ]
}
```

```csharp
// Startup.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    var configurationSection = Configuration.GetSection("FileServers");
    app.UseFileServerPlus(configurationSection);
    ...
}
```

##### Tag Helper 
- asp-file-server-version

```html
<img class="d-block m-auto" src="~/cat.jpg" asp-file-server-version="true" width="200" alt="Sample cat" />
```

##### IUrlHelper
- Url.FileServerContent("PATH")

```html
<img class="d-block m-auto" src="@Url.FileServerContent("~/cat.jpg")" width="200" alt="Sample cat" /> 
``` 

- - - -
#### Multi File Server

```json
// appsettings.json
{
  "FileServers": [
    {
      "ServerId": "Server1",
      "RootDirectory": "..\\storages\\storage1\\",
      "RequestPath": "/storage1",
      "EnableDirectoryBrowsing": false
    },
    {
      "ServerId": "Server2",
      "RootDirectory": "..\\storages\\storage2\\",
      "RequestPath": "/storage2",
      "EnableDirectoryBrowsing": false
    }
  ]
}
```

##### Tag Helper 
- asp-file-server-id

```html
<img class="d-block m-auto" src="~/cat.jpg" asp-file-server-id="Server1" asp-file-server-version="true" width="200" alt="Sample cat" />
```

```html
<img class="d-block m-auto" src="@Url.FileServerContent("Server1","~/cat.jpg")" width="200" alt="Sample cat" />
```