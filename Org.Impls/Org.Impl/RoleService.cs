using Microsoft.Extensions.Configuration;
using Org.Apps;
using Org.Domains.Persons;
using Org.Storages;

namespace Org.Impl
{
    public class RoleService : IRoleService
    {
        private readonly RoleStorage roleStorage;

        public RoleService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ORGDB");
            roleStorage = new RoleStorage(connectionString);
        }

        public async ValueTask<List<Role>> GetRoles()
        {
            return await roleStorage.SelectRoles();
        }

        public async ValueTask<Role?> GetRoleById(Guid roleId)
        {
            return await roleStorage.SelectRoleById(roleId);
        }
    }
}