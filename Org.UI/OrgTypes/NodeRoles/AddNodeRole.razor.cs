using Microsoft.AspNetCore.Components;
using Org.Domains.NodeTypes;
using Org.Domains.Persons;

namespace Org.UI.OrgTypes.NodeRoles;

public partial class AddNodeRole
{
    [Parameter] public EventCallback<NodeRole> OnChildAdded { get; set; }

    private Role roleModel = Role.Create();
    private NodeRole nodeRoleModel = NodeRole.Create();

    public void addNodeRole()
    {
        nodeRoleModel.RoleId = roleModel.RoleId;

        OnChildAdded.InvokeAsync(nodeRoleModel);
    }
}