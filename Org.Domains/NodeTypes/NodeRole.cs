namespace Org.Domains.NodeTypes;

public class NodeRole : IOccurence
{
    public Guid RoleId { get; set; }

    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public NodeRole(Guid roleId, int minValue, int maxValue)
    {
        RoleId = roleId;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public static NodeRole Create(Guid roleId, int min, int max)
    {
        return new NodeRole(roleId, min, max);
    }

    private NodeRole()
    {
        this.RoleId = Guid.NewGuid();
        this.MinValue = 0;
        this.MaxValue = 1;
    }

    public static NodeRole Create() => new();
}