using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IsteerPortal.Data;
using Volo.Abp.DependencyInjection;

namespace IsteerPortal.EntityFrameworkCore;

public class EntityFrameworkCoreIsteerPortalDbSchemaMigrator
    : IIsteerPortalDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreIsteerPortalDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the IsteerPortalDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<IsteerPortalDbContext>()
            .Database
            .MigrateAsync();
    }
}
