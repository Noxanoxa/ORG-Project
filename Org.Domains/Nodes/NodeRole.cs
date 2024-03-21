using Org.Domains.Persons;

namespace Org.Domains.Nodes;

public class NodeRole : IOccurence
{
    public Role Role { get; set; }

    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public NodeRole(Role role, int minValue, int maxValue)
    {
        Role = role;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public static NodeRole Create(Role role, int min, int max)
    {
        return new NodeRole(role, min, max);
    }

    private NodeRole()
    {
        this.Role = null;
        this.MinValue = 0;
        this.MaxValue = 1;
    }

    public static NodeRole Create() => new();
}