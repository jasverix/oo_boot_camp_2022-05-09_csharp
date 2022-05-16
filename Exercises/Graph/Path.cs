namespace Exercises.Graph;

public class Path {
    internal delegate double CostStrategy(Path path);
    internal static readonly CostStrategy LeastCost = (path) => path.Cost();
    internal static readonly CostStrategy FewestHops = (path) => path.Hops();

    private readonly List<Link> _links = new();

    public double Cost() => Link.Cost(_links);

    public int Hops() => _links.Count;

    internal Path Prepend(Link link) {
        _links.Insert(0, link);
        return this;
    }
}
