using Org.Domains.Persons;
using Org.Domains.Shared;

namespace Org.Apps;

public interface IRoleService
{


    //Here
    ValueTask<List<Role>> GetRoles();

    ValueTask<Role?> GetRoleById(Guid roleRoleId);

    ValueTask<Result> CreateRole(Role role);
}