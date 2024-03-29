using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.NodeTypes;
using Org.Domains.Persons;

namespace Org.UI.OrgTypes.NodeTypes;

public partial class NodeTypeEdit
{
    [Inject] public IOrgTypeService orgTypeService { get; set; }
    [Inject] public IRoleService roleService { get; set; }

    private NodeTypeModel nodeType = new NodeTypeModel();

    private List<NodeChild> children = new List<NodeChild>();

    private List<NodeRole> nodeRoles = new List<NodeRole>();

    private List<Role> roles = new List<Role>();

    private NodeChild childToAdd = new NodeChild();
    private bool addChildFormVisible = false;
    private bool addRoleFormVisible = false;
    private bool isAddRoleVisible;

    private async Task createNode()
    {
        NodeType nodeToCreate = new NodeType()
        {
            Id = Guid.NewGuid(),
            Code = nodeType.Code,
            Name = nodeType.Name,
            SubNodes = children,
            Roles = nodeRoles
        };

        await orgTypeService.CreateNodeType(nodeToCreate);
    }

    private void showAddChildForm()
    {
        addChildFormVisible = true;
        StateHasChanged();
    }

    private void showAddroleForm()
    {
        addRoleFormVisible = true;
        StateHasChanged();
    }

    private void showAddRole()
    {
        this.isAddRoleVisible = true;
        StateHasChanged();
    }

    private void addNewChild(NodeChild newChild)
    {
        children.Add(newChild);
        addChildFormVisible = false;
        StateHasChanged();
    }

    private void addNewRole(NodeRole newRole)
    {
        nodeRoles.Add(newRole);
        addRoleFormVisible = false;
        StateHasChanged();
    }

    private void AddNodeRole(NodeRole noderole)
    {
        nodeRoles.Add(noderole);
        isAddRoleVisible = false;
        StateHasChanged();
    }
}