/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph;

// Understands a connection from one Node to another
internal class Link {
    private readonly double _cost;
    private readonly Node _target;

    internal Link(double cost, Node target) {
        _cost = cost;
        _target = target;
    }

    public IEnumerable<Path> AllPaths(Node destination, List<Node> visitedNodes)
        => _target.AllPaths(destination, visitedNodes)
            .Select(path => path.Prepend(this));

    internal static double Cost(IEnumerable<Link> links)
        => links.Sum(l => l._cost);
}
