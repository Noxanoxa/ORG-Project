using Microsoft.AspNetCore.Components;
using Org.Domains.Persons;
using Org.Domains.Nodes;

namespace Org.UI.OrgTypes.NodeRoles;

public partial class AddNodeRole
{
    [Parameter] public EventCallback<NodeRole> OnChildAdded { get; set; }

    private Role roleModel = Role.Create();
    private NodeRole nodeRoleModel = NodeRole.Create();

    public void addNodeRole()
    {
        nodeRoleModel.Role = roleModel;

        OnChildAdded.InvokeAsync(nodeRoleModel);
    }
}