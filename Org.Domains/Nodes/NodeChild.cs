namespace Org.Domains.Nodes;

public class NodeChild : IOccurence
{
    public NodeType NodeType { get; set; } = new NodeType();

    public int MinValue { get; set; }
    public int MaxValue { get; set; }

    public NodeChild(NodeType node, int minValue, int maxValue)
    {
        NodeType = node;
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