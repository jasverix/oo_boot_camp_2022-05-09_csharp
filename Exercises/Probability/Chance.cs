/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using Exercises.Probability;
using Exercises.Sorting;
using ExtensionMethods.Probability;

namespace Exercises.Probability {
    // Understands the likelihood of something specific occurring
    public class Chance : IBetterable {
        private const double EPSILON = 1e-10;
        private const double CERTAIN_FRACTION = 1.0;
        private readonly double _fraction;

        public Chance(double likelihoodAsFraction) {
            if (likelihoodAsFraction is < 0.0 or > 1.0)
                throw new ArgumentException("Value must be between 0.0 and 1.0, inclusive");
            _fraction = likelihoodAsFraction;
        }

        public override bool Equals(object? o) => this == o || o is Chance other && this.Equals(other);

        private bool Equals(Chance other) => Math.Abs(this._fraction - other._fraction) < EPSILON;

        public override int GetHashCode() => Math.Round(_fraction/EPSILON).GetHashCode();

        public bool IsBetter(IBetterable obj)
            => (obj is Chance other) && this._fraction < other._fraction;

        public Chance Not() => new Chance(CERTAIN_FRACTION - _fraction);

        public static Chance operator !(Chance c) => c.Not();

        public Chance And(Chance other) => (this._fraction * other._fraction).Chance();

        public static Chance operator &(Chance left, Chance right) => left.And(right);

        // DeMorgan's Law: https://en.wikipedia.org/wiki/De_Morgan%27s_laws
        public Chance Or(Chance other) => !(!this & !other);

        public static Chance operator |(Chance left, Chance right) => left.Or(right);
    }
}

namespace ExtensionMethods.Probability {
    public static class ChanceConstructors {
        public static Chance Chance(this double fraction) => new Chance(fraction);
        public static Chance Chance(this int wholeNumber) => new Chance(wholeNumber);
    }
}
