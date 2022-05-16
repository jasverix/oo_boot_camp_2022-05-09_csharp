/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph;

// Understands its neighbors
public class Node {
    private readonly List<Link> _links = new List<Link>();

    public bool CanReach(Node destination) => AllPaths(destination).Count > 0;

    public int HopCount(Node destination) => Path(destination, Graph.Path.FewestHops).Hops();

    public double Cost(Node destination) => Path(destination).Cost();

    public Path Path(Node destination) => Path(destination, Graph.Path.LeastCost);

    public List<Path> AllPaths(Node destination) => AllPaths(destination, NoVisitedNodes).ToList();

    private Path Path(Node destination, Path.CostStrategy strategy)
        => AllPaths(destination, NoVisitedNodes)
            .MinBy(path => strategy(path)) ?? throw new ArgumentException("Unreachable destination");

    internal IEnumerable<Path> AllPaths(Node destination, List<Node> visitedNodes) {
        if (this == destination) return new List<Path>() {new Path()};
        if (visitedNodes.Contains(this)) return new List<Path>();
        return _links
            .SelectMany(link => link.AllPaths(destination, CopyWithThis(visitedNodes)));
    }

    private List<Node> CopyWithThis(IEnumerable<Node> originals) => new List<Node>(originals) {this};

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
