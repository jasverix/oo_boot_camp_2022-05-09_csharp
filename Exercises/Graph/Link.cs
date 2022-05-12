/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph {
    // Understands a connection from one Node to another
    class Link {
        private readonly Node _target;

        public Link(Node target) {
            _target = target;
        }

        internal double HopCount(Node destination, List<Node> visitedNodes) => 
            _target.HopCount(destination, visitedNodes) + 1;
    }
}