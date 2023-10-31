using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IsteerPortal;

public class IsteerPortalWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<IsteerPortalWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
