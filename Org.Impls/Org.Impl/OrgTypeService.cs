using System.Transactions;
using Microsoft.Extensions.Configuration;
using Org.Apps;
using Org.Domains.Nodes;
using Org.Domains.Persons;
using Org.Storages;

namespace Org.Impl
{
    public class OrgTypeService : IOrgTypeService
    {
        private readonly IRoleService roleService;
        private readonly OrgTypeStorage orgTypeStorage;

        public OrgTypeService(
            IConfiguration configuration,
            IRoleService roleService
            )
        {
            this.roleService = roleService;
            string connectionString = configuration.GetConnectionString("db_aa6b7b_orgteame");
            orgTypeStorage = new OrgTypeStorage(connectionString);
        }

        public async ValueTask CreateNodeType(NodeType nodeType)
        {
            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await validateNodeForInsertion(nodeType);

                await orgTypeStorage.InsertNodeType(nodeType.Id, nodeType.Code, nodeType.Name);

                foreach (NodeRole role in nodeType.Roles)
                {
                    await orgTypeStorage.AddRoleToNodeType(nodeType.Id, role);
                }

                foreach (NodeChild child in nodeType.SubNodes)
                {
                    await orgTypeStorage.AddSubNodeToNodeType(nodeType.Id, child);
                }

                scope.Complete();
            }
            finally
            {
                scope.Dispose();
            }
        }

        private async ValueTask validateNodeForInsertion(NodeType nodeType)
        {
            if (await GetNodeTypeById(nodeType.Id) is not null)
                throw new Exception("ID du Noeud existe déja");

            if (await GetNodeTypeByCode(nodeType.Code) is not null)
                throw new Exception("Code du noeud existe déja");

            foreach (NodeRole nodeRole in nodeType.Roles)
            {
                if (await roleService.GetRoleById(nodeRole.Role.RoleId) is null)
                {
                    throw new Exception("Le role n'existe pas");
                }
            }

            foreach (NodeChild nodeChild in nodeType.SubNodes)
            {
                if (await GetNodeTypeById(nodeChild.NodeType.Id) is null)
                {
                    throw new Exception("Le sous noued n'existe pas");
                }
            }
        }

        public async ValueTask<NodeType?> GetNodeTypeById(Guid id)
        {
            return await orgTypeStorage.SelectNodeTypeById(id);
        }

        public async ValueTask<NodeType?> GetNodeTypeByCode(string code)
        {
            return await orgTypeStorage.SelectNodeTypeByCode(code);
        }

        public async ValueTask<List<NodeType>> GetNodeTypes()
        {
            return await orgTypeStorage.SelectNodeTypes();
        }
    }
}