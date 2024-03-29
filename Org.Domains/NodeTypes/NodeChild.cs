namespace Org.Domains.NodeTypes;

public class NodeChild : IOccurence
{
    public Guid NodeTypeId { get; set; }

    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public NodeChild(Guid childId, int minValue, int maxValue)
    {
        NodeTypeId = childId;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public NodeChild()
    {
    }
}

public interface IOccurence
{
    int MinValue { get; set; }
    int MaxValue { get; set; }
}