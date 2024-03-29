using Microsoft.AspNetCore.Components;
using Org.Apps;
using Org.Domains.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.Domains.NodeTypes;

namespace Org.UI.OrgTypes.NodeTypes
{
    public partial class NodeRoleEdit
    {
        private List<Role> roles = new List<Role>();
        [Inject] public IRoleService RoleService { get; set; }

        [Parameter] public EventCallback<NodeRole> OnRoleAdded { get; set; }

        private Role nodeRole = Role.Create();
        private int minValue = 0;
        private int maxValue = 0;

        protected override async Task OnInitializedAsync()
        {
            roles = await RoleService.GetRoles();
        }

        private void roleAdded()
        {
            NodeRole role = NodeRole.Create(roles.FirstOrDefault(n => n.RoleId == nodeRole.RoleId).RoleId, minValue,
                maxValue);

            OnRoleAdded.InvokeAsync(role);
        }
    }
}