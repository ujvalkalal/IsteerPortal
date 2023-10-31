using System.Threading.Tasks;

namespace IsteerPortal.Data;

public interface IIsteerPortalDbSchemaMigrator
{
    Task MigrateAsync();
}
