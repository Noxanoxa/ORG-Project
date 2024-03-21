using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.Nodes;
using Org.Domains.Persons;

namespace Org.Portal.Pages.OrgTypes;

public partial class CreateNodeTypePage
{
    [Inject] public IOrgTypeService orgTypeService { get; set; }

    private NodeType nodeToAdd = new NodeType();

    private string errorMessage = String.Empty;
    private bool hasError = false;

    protected override void OnInitialized()
    {
        nodeToAdd.Id = Guid.NewGuid();
        nodeToAdd.Code = "FAC";
        nodeToAdd.Name = "Faculté";

        nodeToAdd.Roles.Add(new NodeRole(Role.Create("DOYEN", "Doyen de faculté"), 1, 1));
        nodeToAdd.Roles.Add(new NodeRole(Role.Create("VDP", "Vice-doyen Pédagogie"), 1, 1));
        nodeToAdd.Roles.Add(new NodeRole(Role.Create("VDPG", "Vice-Doyen Post-Grad"), 1, 1));

        nodeToAdd.SubNodes.Add(new NodeChild(NodeType.Create("DEP", "Département"), 1, 0));
        nodeToAdd.SubNodes.Add(new NodeChild(NodeType.Create("LAB", "Laboratoire de recherche"), 1, 0));
    }

    private async Task createNodeType()
    {
        hasError = false;
        try
        {
            await orgTypeService.CreateNodeType(nodeToAdd);
        }
        catch (Exception e)
        {
            hasError = true;
            errorMessage = e.Message;
        }
    }
}