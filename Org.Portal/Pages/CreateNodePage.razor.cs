using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.Nodes;
using Org.Domains.NodeTypes;
using Org.Domains.Persons;

namespace Org.Portal.Pages
{
    public partial class CreateNodePage
    {
        [Inject] private IOrgTypeService orgTypeService { get; set; }
        [Inject] private IRoleService roleService { get; set; }

        private List<NodeType> nodeTypes;
        private List<Role> roles;

        private NodeType selectedType;

        private bool creatingNode = false;
        private bool hasError = false;
        private string errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            nodeTypes = await orgTypeService.GetNodeTypes();
            roles = await roleService.GetRoles();
        }

        private string nodeName(Guid id)
        {
            NodeType node = nodeTypes.First(r => r.Id == id);
            return $"{node.Code} - {node.Name}";
        }

        private string roleName(Guid id)
        {
            Role role = roles.First(r => r.RoleId == id);
            return $"{role.RoleCode} - {role.RoleName}";
        }

        private Guid selectedId;

        private async Task selectionChanged(ChangeEventArgs obj)
        {
            hasError = false;
            try
            {
                if (obj is not null)
                {
                    selectedId = Guid.Parse((string)obj.Value);
                    selectedType = await orgTypeService.GetNodeTypeById(selectedId);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                hasError = true;
            }
        }

        private Node nodeToCreate;

        private void showCreatingNodeForm()
        {
            creatingNode = true;
            nodeToCreate = Node.Create(selectedId);
        }

        private NodePerson personToCreate;
        private Guid selectedRole;

        private void personRoleChanged(ChangeEventArgs obj)
        {
            if (obj is not null)
            {
                selectedRole = Guid.Parse((string)obj.Value);
            }
        }

        private bool addPersonVisible = false;

        private void showAddPerson()
        {
            personToCreate = new NodePerson();
            personToCreate.RoleId = selectedRole;
            addPersonVisible = true;
        }

        private void addPersonToNode()
        {
            nodeToCreate.Persons.Add(personToCreate);
            addPersonVisible = false;
        }

        private List<Role> getAllowedRoles()
        {
            List<Role> result = new List<Role>();
            foreach (NodeRole role in selectedType.Roles)
            {
                int maxRoles = selectedType.Roles.First(r => r.RoleId == role.RoleId).MaxValue;
                if (nodeToCreate.Persons.Count(p => p.RoleId == role.RoleId) < maxRoles || maxRoles == 0)
                {
                    result.Add(roles.First(r => r.RoleId == role.RoleId));
                }
            }

            return roles;
        }
    }
}