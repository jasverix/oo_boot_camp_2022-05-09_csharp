/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph {
    // Understands its neighbors
    public class Node {
        private const double Unreachable = double.PositiveInfinity;
        private readonly List<Link> _links = new List<Link>();

        public bool CanReach(Node destination) => Cost(destination, NoVisitedNodes, Link.FewestHops) != Unreachable;

        public int HopCount(Node destination) => (int)Cost(destination, Link.FewestHops);

        public double Cost(Node destination) => Cost(destination, Link.LeastCost);

        private double Cost(Node destination, Link.CostStrategy strategy) {
            var result = Cost(destination, NoVisitedNodes, strategy);
            if (result == Unreachable) throw new ArgumentException("Destination cannot be reached");
            return result;
        }

        internal double Cost(Node destination, List<Node> visitedNodes, Link.CostStrategy strategy) {
            if (this == destination) return 0;
            if (visitedNodes.Contains(this) || _links.Count == 0) return Unreachable;
            return _links.Min((link) => link.Cost(destination, CopyWithThis(visitedNodes), strategy));
        }

        private static readonly FakePath UnreachablePath = new FakePath();

        public Path Path(Node destination) {
            var result = Path(destination, NoVisitedNodes);
            if (result == UnreachablePath) throw new ArgumentException("Unreachable destination");
            return result;
        }

        internal Path Path(Node destination, List<Node> visitedNodes) {
            if (destination == this) return new RealPath();
            if (visitedNodes.Contains(this) || _links.Count == 0) return UnreachablePath;
            return _links
                .Select(link => link.Path(destination, CopyWithThis(visitedNodes)))
                .MinBy(path => path.Cost()) ?? UnreachablePath;
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
}
