using Org.Domains.Nodes;

namespace Org.Apps;

public interface INodeService
{
    Task CreateNode(Node node);
}