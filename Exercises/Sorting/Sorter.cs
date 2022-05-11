namespace Exercises.Sorting;

public interface IBetterable<T> {
    public bool IsBetter(T other);
}

public static class ListExtensions {
    public static T? GetBest<T>(this List<T> items) where T: IBetterable<T> {
        if (items.Count == 0) {
            return default(T);
        }
        var champion = items[0];
        for (var i = 1; i < items.Count; ++i) {
            var challenger = items[i];
            if (challenger.IsBetter(champion)) champion = challenger;
        }

        return champion;
    }
}
