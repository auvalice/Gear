using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gear.Extensions;

/// <summary>
/// Extensions to make the web application builder a little more readable.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configure the application's properties as you would in <code>Configure(app)</code> in a standard ASP.NET app.
    /// </summary>
    /// <param name="app">The web application builder to perform the action on</param>
    /// <param name="action">Actions to perform on the web application builder</param>
    /// <returns>The web application builder for method chaining</returns>
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder app,
        Action<WebApplicationBuilder> action)
    {
        action(app);
        return app;
    }

    /// <summary>
    /// Configure the application's services as you would in <code>ConfigureServices(service)</code> in a standard ASP.NET app.
    /// </summary>
    /// <param name="app">The web application builder that contains the service container to register services in</param>
    /// <param name="action">Actions to perform when configuring services</param>
    /// <returns>The web application builder for method chaining</returns>
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder app,
        Action<IServiceCollection> action)
    {
        action(app.Services);
        return app;
    }
}