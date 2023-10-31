using IsteerPortal.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace IsteerPortal.Permissions;

public class IsteerPortalPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IsteerPortalPermissions.GroupName);

        myGroup.AddPermission(IsteerPortalPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(IsteerPortalPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(IsteerPortalPermissions.MyPermission1, L("Permission:MyPermission1"));

        var customerPermission = myGroup.AddPermission(IsteerPortalPermissions.Customers.Default, L("Permission:Customers"));
        customerPermission.AddChild(IsteerPortalPermissions.Customers.Create, L("Permission:Create"));
        customerPermission.AddChild(IsteerPortalPermissions.Customers.Edit, L("Permission:Edit"));
        customerPermission.AddChild(IsteerPortalPermissions.Customers.Delete, L("Permission:Delete"));

        var customerAddresPermission = myGroup.AddPermission(IsteerPortalPermissions.CustomerAddress.Default, L("Permission:CustomerAddress"));
        customerAddresPermission.AddChild(IsteerPortalPermissions.CustomerAddress.Create, L("Permission:Create"));
        customerAddresPermission.AddChild(IsteerPortalPermissions.CustomerAddress.Edit, L("Permission:Edit"));
        customerAddresPermission.AddChild(IsteerPortalPermissions.CustomerAddress.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IsteerPortalResource>(name);
    }
}