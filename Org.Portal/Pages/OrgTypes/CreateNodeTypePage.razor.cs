using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.NodeTypes;
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