using StacyClouds.SwaAuth.Models;
namespace Api;

internal class RoleProcessor : IRoleProcessor
{
    public List<string> ProcessRoles(ClientPrincipal clientPrincipal)
    {
        if (clientPrincipal.Claims is null)
        {
            return [];
        }

        return [.. clientPrincipal
            .Claims
            .Where(
              claim =>
                claim.Typ == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            .Select(claim => claim.Val)];
    }
}
