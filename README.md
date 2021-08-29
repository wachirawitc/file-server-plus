## File server extensions of ASP.NET Core

An extensions of file server on ASP.NET core to support file version.

##### Normal (Framework not generate version if use file server)
```html
<img class="d-block m-auto" src="/storage1/cat.jpg" width="200" alt="Sample cat">
```

##### File server Plus
```html
<img class="d-block m-auto" src="/storage1/cat.jpg?v=vW8o-DqXyVVm8IQWxIOh8eFuv91UvMW9uJ2ei1a2vX8" width="200" alt="Sample cat">
```

## Get Started

##### Register

###### 1. Views/_ViewImports.cshtml

```csharp
// _ViewImports.cshtml
@using FileServerPlus.Mvc.Extensions
... 
@addTagHelper *, FileServerPlus.Mvc
```

###### 2. Configure services 
```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    ...

    services.AddFileServerPlus();
}
```

###### 3. Configure directory

###### 3.1 Sample 
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

###### 3.2 Use configure from json file 

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

## Options
###### 1. Tag Helper 
- asp-file-server-version

```html
<img class="d-block m-auto" src="~/cat.jpg" asp-file-server-version="true" width="200" alt="Sample cat" />
```

###### 2. IUrlHelper
- Url.FileServerContent("PATH")

```html
<img class="d-block m-auto" src="@Url.FileServerContent("~/cat.jpg")" width="200" alt="Sample cat" /> 
``` 


###### 3. File server context
- Get
- GetUrl

```csharp
    // HomeController.cs
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var fileInfo1 = _fileServerPlusContext.Get(@"/cat.jpg");
            var fileInfo2 = _fileServerPlusContext.Get(@"~/cat.jpg");

            var fileUrl = _fileServerPlusContext.GetUrl(@"/cat.jpg");
            
            return View();
        }

        private readonly IFileServerPlusContext _fileServerPlusContext;

        public HomeController(IFileServerPlusContext fileServerPlusContext)
        {
            _fileServerPlusContext = fileServerPlusContext;
        }
    }
```

## Multi File server

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

###### Selecte sever
- asp-file-server-id

```html
<img class="d-block m-auto" src="~/cat.jpg" asp-file-server-id="Server1" asp-file-server-version="true" width="200" alt="Sample cat" />
```

```html
<img class="d-block m-auto" src="@Url.FileServerContent("Server1","~/cat.jpg")" width="200" alt="Sample cat" />
```