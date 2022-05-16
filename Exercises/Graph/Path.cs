namespace Exercises.Graph;

public abstract class Path {
    internal delegate double CostStrategy(Path path);
    internal static readonly CostStrategy LeastCost = (path) => path.Cost();
    internal static readonly CostStrategy FewestHops = (path) => path.Hops();

    public abstract double Cost();
    public abstract int Hops();

    internal abstract Path Prepend(Link link);

    internal class RealPath : Path {
        private readonly List<Link> _links = new();

        public override double Cost() => Link.Cost(_links);

        public override int Hops() => _links.Count;

        internal override Path Prepend(Link link) {
            _links.Insert(0, link);
            return this;
        }
    }

    private class FakePath : Path {
        public override double Cost() => double.PositiveInfinity;

        public override int Hops() => int.MaxValue;

        internal override Path Prepend(Link link) => this;
    }

    internal static readonly Path Unreachable = new FakePath();
}
