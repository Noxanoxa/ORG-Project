namespace Org.Domains.Nodes;

public class NodeType
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public List<NodeRole> Roles { get; set; }
    public List<NodeChild> SubNodes { get; set; }

    public NodeType()
    {
        Roles = new List<NodeRole>();
        SubNodes = new List<NodeChild>();
    }

    private NodeType(Guid id, string code, string name) : this()
    {
        Id = id;
        Code = code;
        Name = name;
    }

    public static NodeType Create(Guid id, string code, string name) => new(id, code, name);

    public static NodeType Create(string code, string name) => new(Guid.NewGuid(), code, name);
}