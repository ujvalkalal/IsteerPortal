using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace IsteerPortal.Data;

/* This is used if database provider does't define
 * IIsteerPortalDbSchemaMigrator implementation.
 */
public class NullIsteerPortalDbSchemaMigrator : IIsteerPortalDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
