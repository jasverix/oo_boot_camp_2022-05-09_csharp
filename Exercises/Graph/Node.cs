/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph;

// Understands its neighbors
public class Node {
    private static readonly Path Unreachable = Graph.Path.Unreachable;

    private readonly List<Link> _links = new List<Link>();

    public bool CanReach(Node destination) => Path(destination, NoVisitedNodes, Graph.Path.FewestHops) != Unreachable;

    public int HopCount(Node destination) => Path(destination, Graph.Path.FewestHops).Hops();

    public double Cost(Node destination) => Path(destination).Cost();

    public Path Path(Node destination) => Path(destination, Graph.Path.LeastCost);

    private Path Path(Node destination, Path.CostStrategy strategy) {
        var result = Path(destination, NoVisitedNodes, strategy);
        if (result == Unreachable) throw new ArgumentException("Unreachable destination");
        return result;
    }

    internal Path Path(Node destination, List<Node> visitedNodes, Path.CostStrategy strategy) {
        if (destination == this) return new Path.RealPath();
        if (visitedNodes.Contains(this) || _links.Count == 0) return Unreachable;
        return _links
            .Select(link => link.Path(destination, CopyWithThis(visitedNodes), strategy))
            .MinBy(path => strategy(path)) ?? Unreachable;
    }

    public List<Path> AllPaths(Node destination) {
        return AllPaths(destination, NoVisitedNodes);
    }

    internal List<Path> AllPaths(Node destination, List<Node> visitedNodes) {
        if (this == destination) return new List<Path>() {new Path.RealPath()};
        if (visitedNodes.Contains(this) || _links.Count == 0) return new List<Path>();
        return _links
            .SelectMany(link => link.AllPaths(destination, CopyWithThis(visitedNodes)))
            .Where(path => path != Unreachable)
            .ToList();
    }

    private List<Node> CopyWithThis(List<Node> originals) => new List<Node>(originals) { this };

    private static List<Node> NoVisitedNodes => new();

    public LinkBuilder Cost(double amount) => new LinkBuilder(amount, _links);

    public class LinkBuilder {
        private readonly double _cost;
        private readonly List<Link> _links;

        internal LinkBuilder(double cost, List<Link> links) {
            _cost = cost;
            _links = links;
        }

        public Node To(Node neighbor) {
            _links.Add(new Link(_cost, neighbor));
            return neighbor;
        }

    }
}
