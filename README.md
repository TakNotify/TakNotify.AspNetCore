[![Build Status](https://dev.azure.com/taknotify/TakNotify/_apis/build/status/TakNotify.AspNetCore?branchName=master)](https://dev.azure.com/taknotify/TakNotify/_build/latest?definitionId=4&branchName=master)
![Nuget](https://img.shields.io/nuget/v/taknotify.aspnetcore)

# TakNotify.AspNetCore

`TakNotify.AspNetCore` package is the extension of `TakNotify` that provides functionality to set up and integrate the `TakNotify` service and the `TakNotify` providers into the ASP.NET Core Dependency Injection system.

## NuGet Package

This library is available as a NuGet package in https://www.nuget.org/packages/TakNotify.AspNetCore and it has dependency to the [TakNotify core library](https://www.nuget.org/packages/TakNotify).

It can be installed easily via the `Manage NuGet Packages` menu in Visual Studio or by using the `dotnet add package` command in command line:

```powershell
dotnet add package TakNotify.AspNetCore
```

## Build the source code

If for some reasons you need to build the `TakNotify.AspNetCore` library from the code yourself, you can always use the usual `dotnet build` command because basically it's just a .NET Core class library:

```powershell
dotnet build .\src\TakNotify.AspNetCore
```

However, we recommend you to just use the `build.ps1` script because it will not only help you to build the code, but also publish it into a ready to use NuGet package.

```powershell
.\build.ps1
```

## Usage

This library is used to setup `TakNotify` in an ASP.NET Core application. This is done in the `ConfigureServices()` method in the `Startup` class. There are two things that you can do with this library:
1. Setup the `TakNotify` service
2. Register the `TakNotify` providers

### Setup the `TakNotify` service

You can easily setup the `TakNotify` service to be integrated into your application by calling the `AddTakNotify()` method. 

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddTakNotify();
}
```

This is an extension method of `IServiceCollection` that will make `INotification` object to be available for injection from the entire of your application code.

### Register the `TakNotify` providers

The `TakNotify` providers can be registered easily by calling the `AddProvider<TProvider, TOptions>()` method. The `<TProvider>` parameter is the type of provider that you want to register, e.g. `SmtpProvider`, `SendGridProvider`, etc. The `<TOptions>` parameter is the type of options for the provider. Every provider has its own options, e.g. `SmtpProviderOptions`, `SendGridOptions`, etc.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddTakNotify();
        .AddProvider<SmtpProvider, SmtpProviderOptions>(options =>
        {
            options.Server = "";
            options.Port = "";
            options.Username = "";
            options.Password = "";
            options.UseSSL = "";
            options.DefaultFromAddress = "";
        });
}
```

This method is part of the `NotificationBuilder` object which is the return value of `AddTakNotify()` method. You can call this methods multiple times to register multiple `TakNotify` providers into your application. Please see the [samples](https://github.com/TakNotify/Samples/blob/master/src/Web/Startup.cs).

## Next

Please refer to the [project page](https://taknotify.github.io/) to get
more info about the `TakNotify` ecosystem.
