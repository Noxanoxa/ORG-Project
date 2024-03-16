using Org.Domains.Nodes;

namespace Org.Apps;

public interface IOrgTypeService
{
    ValueTask CreateNodeType(NodeType nodeType);

    ValueTask<NodeType?> GetNodeTypeById(Guid id);

    ValueTask<NodeType?> GetNodeTypeByCode(string code);

    ValueTask<List<NodeType>> GetNodeTypes();
}