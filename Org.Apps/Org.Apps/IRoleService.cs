using Org.Domains.Persons;

namespace Org.Apps;

public interface IRoleService
{
    ValueTask<List<Role>> GetRoles();

    ValueTask<Role?> GetRoleById(Guid roleRoleId);
    ValueTask<bool> CreateRole(Guid roleId, string roleCode, string roleName);
}