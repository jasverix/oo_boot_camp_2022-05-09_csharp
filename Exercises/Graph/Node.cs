/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph;

// Understands its neighbors
public class Node {
    private const double Unreachable = double.PositiveInfinity;
    private readonly List<Edge> _edges = new List<Edge>();

    public bool CanReach(Node destination) => Cost(destination, NoVisitedNodes(), Edge.HopStrategy) != Unreachable;

    public int HopCount(Node destination) => (int)Cost(destination, Edge.HopStrategy);

    public double MinCost(Node destination) => Cost(destination, Edge.CostStrategy);

    private double Cost(Node destination, Func<double, double> strategy) {
        var result = Cost(destination, NoVisitedNodes(), strategy);
        if (result == Unreachable) throw new ArgumentException("Destination cannot be reached");
        return result;
    }

    private double Cost(Node destination, List<Node> visitedNodes, Func<double, double> strategy) {
        if (this == destination) return 0.0;
        if (visitedNodes.Contains(this)) return Unreachable;
        if (_edges.Count == 0) return Unreachable;
        return _edges.Min(e => e.Cost(destination, CopyWithThis(visitedNodes), strategy));
    }

    private List<Node> CopyWithThis(List<Node> originals) => new List<Node>(originals) {this};

    private static List<Node> NoVisitedNodes() => new();

    public class Edge {
        public static readonly Func<double, double> CostStrategy = e => e;
        public static readonly Func<double, double> HopStrategy = e => 1;

        private readonly Node _node;
        private readonly double _cost;

        public Edge(Node node, double cost) {
            _node = node;
            _cost = cost;
        }

        internal double Cost(Node destination, List<Node> visitedNodes, Func<double, double> strategy)
            => strategy(this._cost) + _node.Cost(destination, visitedNodes, strategy);
    }

    public EdgeBuilder Cost(double amount) => new EdgeBuilder(amount, _edges);

    public class EdgeBuilder {
        private readonly double _cost;
        private readonly List<Edge> _edges;

        public EdgeBuilder(double cost, List<Edge> edges) {
            _cost = cost;
            _edges = edges;
        }

        public Node To(Node neighbor) {
            _edges.Add(new Edge(neighbor, _cost));
            return neighbor;
        }
    }

}
