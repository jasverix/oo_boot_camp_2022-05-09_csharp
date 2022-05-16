namespace Exercises.Graph;

public abstract class Path {
    public abstract double Cost();
    public abstract int Hops();

    internal abstract Path Prepend(Link link);
}

internal class RealPath : Path {
    private readonly List<Link> _links = new();

    public override double Cost() => Link.Cost(_links);

    public override int Hops() => _links.Count;

    internal override Path Prepend(Link link) {
        _links.Insert(0, link);
        return this;
    }
}

internal class FakePath : Path {
    public override double Cost() => double.PositiveInfinity;

    public override int Hops() => int.MaxValue;

    internal override Path Prepend(Link link) => this;
}
