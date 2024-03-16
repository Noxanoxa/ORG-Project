using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.Nodes;

namespace Org.UI.OrgTypes.NodeTypes
{
    public partial class SubNodeEdit
    {
        [Inject] public IOrgTypeService orgTypeService { get; set; }

        [Parameter] public EventCallback<NodeChild> OnChildAdded { get; set; }

        private NodeType subNode = new NodeType();
        private int minValue = 0;
        private int maxValue = 0;

        private List<NodeType> nodes = new List<NodeType>();

        private bool loadCompleted = false;

        protected override async Task OnInitializedAsync()
        {
            loadCompleted = false;
            nodes = await orgTypeService.GetNodeTypes();
            loadCompleted = true;
        }

        private void childAdded()
        {
            NodeChild child = new NodeChild()
            {
                NodeType = nodes.FirstOrDefault(n => n.Id == subNode.Id),
                MinValue = minValue,
                MaxValue = maxValue
            };
            OnChildAdded.InvokeAsync(child);
        }
    }
}