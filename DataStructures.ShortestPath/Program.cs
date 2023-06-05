using System.Text;


#region Node Creation

var nodeA = new Node { Name = "A" };
var nodeB = new Node { Name = "B" };
var nodeC = new Node { Name = "C" };
var nodeD = new Node { Name = "D" };
var nodeE = new Node { Name = "E" };
var nodeF = new Node { Name = "F" };
var nodeG = new Node { Name = "G" };

nodeA.Add(nodeB, 2);
nodeA.Add(nodeC, 4);
nodeA.Add(nodeF, 5);
nodeA.Add(nodeD, 7);

nodeB.Add(nodeA, 2);
nodeB.Add(nodeD, 6);
nodeB.Add(nodeE, 3);
nodeB.Add(nodeG, 8);

nodeC.Add(nodeA, 4);
nodeC.Add(nodeF, 6);

nodeD.Add(nodeA, 7);
nodeD.Add(nodeB, 6);
nodeD.Add(nodeF, 1);
nodeD.Add(nodeG, 6);

nodeE.Add(nodeB, 3);
nodeE.Add(nodeG, 7);

nodeF.Add(nodeA, 5);
nodeF.Add(nodeG, 6);
nodeF.Add(nodeC, 6);
nodeF.Add(nodeD, 1);

#endregion

var nodes = new List<Node> { nodeA, nodeB, nodeC, nodeD, nodeE, nodeF, nodeG };
var parents = new Dictionary<Node, Node>();
var distances = new Dictionary<Node, int?>();

foreach (var node in nodes)
    distances.Add(node, int.MaxValue);

var currentNode = nodeA;
distances[currentNode] = 0;

var destinationNode = nodeD;

while (currentNode is not null)
{
    var nodeEdges = currentNode.GetEdges();

    foreach (var edge in nodeEdges)
    {
        var currentValue = distances[currentNode].Value;
        var valueToEdge = currentNode.GetDistanceTo(edge);
        var edgeValue = distances[edge].Value;
        long totalVal = currentValue + valueToEdge;

        var minVal = Math.Min(totalVal, edgeValue);

        if (minVal < edgeValue)
        {
            distances[edge] = (int)minVal;
            parents[edge] = currentNode;
        }
    }

    currentNode.IsDiscovered = true;

    if (currentNode == destinationNode)
    {
        break;
    }

    currentNode = nodes
                    .Where(i => !i.IsDiscovered)
                    .MinBy(i => distances[currentNode]);
}

var totalDistance = distances[currentNode];
var pathNodes = new List<Node>();

while (destinationNode is not null)
{
    pathNodes.Add(destinationNode);

    if (destinationNode is not null)
        parents.TryGetValue(destinationNode, out destinationNode);
}


Console.WriteLine(string.Join(" -> ", pathNodes.Select(i => i.Name)));
Console.WriteLine("Total Distance: {0}", totalDistance);


Console.ReadLine();

class Node
{
    private Dictionary<Node, int> edges = new();

    public bool IsDiscovered { get; set; }

    public required string Name { get; set; }


    public Node Add(Node node, int distance)
    {
        edges.TryAdd(node, distance);
        return this;
    }

    public IEnumerable<Node> GetEdges()
    {
        return edges.Keys;
    }

    public int GetDistanceTo(Node node)
    {
        return edges.TryGetValue(node, out var distance) ? distance : 0;
    }

    public override string ToString() => $"Node: {Name}";
}