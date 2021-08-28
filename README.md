### File Server extensions of ASP.NET Core

An extensions of file server on ASP.NET core to support file version
 
#### Get Started

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...

    var fileStorage1 = new DirectoryInfo("Storage\\FileStorage1\\");
    app.UseFileServerPlus(fileStorage1, "/storage1", enableDirectoryBrowsing: true);

    var fileStorage2 = new DirectoryInfo("Storage\\FileStorage2\\");
    app.UseFileServerPlus(fileStorage2, "/storage2", enableDirectoryBrowsing: true);

    ...
}
```

#### Tag Helper 
- asp-file-server-version

```html
<img src="~/storage1/cat.jpg" asp-file-server-version="true" width="200" alt="Sample cat" />
```

#### IUrlHelper
- Url.FileServerContent("PATH")

```html
<img src="@Url.FileServerContent("~/storage1/cat.jpg")" width="200" alt="Sample cat" />
```