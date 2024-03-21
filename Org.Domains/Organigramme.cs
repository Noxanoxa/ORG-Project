using Org.Domains.Nodes;

namespace Org.Domains;

public class Organigramme
{
    public NodeType Root { get; set; }
    public List<NodeChild> Children { get; set; }

    public Organigramme()
    {
        Root = new NodeType();
        Children = new List<NodeChild>();
    }
}