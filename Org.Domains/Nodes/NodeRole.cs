using Org.Domains.Persons;

namespace Org.Domains.Nodes;

public class NodeRole : IOccurence
{
    public Role Role { get; set; } = new Role();

    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public NodeRole(Role role, int minValue, int maxValue)
    {
        Role = role;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}