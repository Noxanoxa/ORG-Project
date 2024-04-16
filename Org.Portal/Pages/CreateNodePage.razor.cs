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
        [Inject] private INodeService nodeService { get; set; }

        private List<NodeType> nodeTypes;
        private List<Role> roles;
        private List<Node> nodes;

        private NodeType selectedType;

        private bool creatingNode = false;
        private bool hasError = false;
        private string errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            nodeTypes = await orgTypeService.GetNodeTypes();
            roles = await roleService.GetRoles();
            nodes = await nodeService.GetNodes();
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
        private Node subnodeToCreate;


        private void showCreatingNodeForm()
        {
            creatingNode = true;
            nodeToCreate = Node.Create(selectedId);
            allowedRoles = getAllowedRoles();
            allowedNodes  = getAllowedNodes();
        }

        private NodePerson personToCreate;
        private Guid selectedRole;
        private Guid selectedNode;

        //*****************************
        private void personRoleChanged(ChangeEventArgs obj)
        {
            if (obj is not null)
            {
                personToCreate.RoleId = Guid.Parse((string)obj.Value);
            }
        }

        //***************************** 

        private bool addPersonVisible = false;
        private bool addSubNodeVisible = false;

        private void showAddPerson()
        {
            allowedRoles?.Clear();
            personToCreate = new NodePerson();
            personToCreate.RoleId = selectedRole;
            allowedRoles = getAllowedRoles();
            addPersonVisible = true;
        }
        private void showAddSubNode()
        {
            allowedNodes?.Clear();
            subnodeToCreate = new Node();
            subnodeToCreate.NodeId = selectedNode;
            allowedNodes = getAllowedNodes();
            addSubNodeVisible = true;
        }

        private void addPersonToNode()
        {
            if (personToCreate.PersonId == Guid.Empty ||
                personToCreate.RoleId == Guid.Empty ||
                string.IsNullOrWhiteSpace(personToCreate.Nom))
            {
                addPersonVisible = false;
            }
            else
            {
                nodeToCreate.Persons.Add(personToCreate);
                allowedRoles = getAllowedRoles();
                
                addPersonVisible = false;
            }
        }
        private void addSubNodeToNode()
        {
            if (subnodeToCreate.NodeId == Guid.Empty ||
                string.IsNullOrWhiteSpace(subnodeToCreate.Code) ||
                string.IsNullOrWhiteSpace(subnodeToCreate.Name))
            {
                addSubNodeVisible = false;
            }
            else
            {
                nodeToCreate.SubNodes.Add(subnodeToCreate);
                allowedNodes = getAllowedNodes();
                
                addSubNodeVisible = false;
            }
        }

        private List<Role> allowedRoles;
        private List<Node> allowedNodes;

        private List<Role> getAllowedRoles()
        {
            List<Role> result = new List<Role>();
            foreach (NodeRole role in selectedType.Roles)
            {
                int maxRoles = role.MaxValue;
                if (nodeToCreate.Persons.Count(p => p.RoleId == role.RoleId) < maxRoles || maxRoles == 0)
                {
                    result.Add(roles.FirstOrDefault(r => r.RoleId == role.RoleId));
                }
            }

            return result;
        }


                private List<Node> getAllowedNodes()
        {
            List<Node> result = new List<Node>();
            foreach (NodeChild node in selectedType.SubNodes)
            {
                int maxNodes = node.MaxValue;
                if (nodeToCreate.SubNodes.Count(p => p.NodeId == node.NodeTypeId) < maxNodes || maxNodes == 0)
                {
                    result.Add(nodes.FirstOrDefault(n => n.TypeId == node.NodeTypeId));
                    
                }
            }

            return result;
        }

        private async Task saveNode()
        {
            await nodeService.CreateNode(nodeToCreate);
            foreach (NodePerson person in nodeToCreate.Persons)
            {
                await nodeService.AddPersonToNode(nodeToCreate.NodeId, person);
            }
            foreach (Node subnode in nodeToCreate.SubNodes)
            {
                await nodeService.AddSubNodeToNode(nodeToCreate.NodeId, subnode);
            }
        }

        private void handleNodeCreated(Node nodeToHandle)
        {
            nodeToCreate.Code = nodeToHandle.Code;
            nodeToCreate.Name = nodeToHandle.Name;
        }
    }
}