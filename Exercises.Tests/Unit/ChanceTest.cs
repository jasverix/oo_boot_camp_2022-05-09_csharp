/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using System.Collections.Generic;
using Exercises.Probability;
using Exercises.Sorting;
using ExtensionMethods.Probability;
using Xunit;

namespace Exercises.Tests.Unit {

// Ensures that Chance operates correctly
    public class ChanceTest {
        private static readonly Chance Impossible = 0.Chance();
        private static readonly Chance Unlikely = 0.25.Chance();
        private static readonly Chance EquallyLikely = 0.5.Chance();
        private static readonly Chance Likely = 0.75.Chance();
        private static readonly Chance Certain = 1.Chance();

        [Fact]
        public void Equality() {
            Assert.Equal(Likely, new Chance(0.75));
            Assert.NotEqual(Likely, Unlikely);
            Assert.NotEqual(Likely, new object());
            Assert.NotEqual(Likely, null);
        }

        [Fact]
        public void Set() {
            Assert.Single(new HashSet<Chance> { Likely, 0.75.Chance() });
            Assert.Contains(0.75.Chance(), new HashSet<Chance> { Likely });
        }

        [Fact]
        public void Hash() {
            Assert.Equal(0.75.Chance().GetHashCode(), Likely.GetHashCode());
            Assert.Equal(0.3.Chance().GetHashCode(), (!!0.3.Chance()).GetHashCode());
        }

        [Fact]
        public void Not() {
            Assert.Equal(Unlikely, !Likely);
            Assert.Equal(EquallyLikely, !EquallyLikely);
            Assert.Equal(Likely, !!Likely);
            Assert.Equal(Likely, Likely.Not().Not());
            Assert.Equal(Impossible, !Certain);
            Assert.Equal(Certain, !Impossible);
            Assert.Equal(0.3.Chance(), !!0.3.Chance());
        }
        [Fact]
        public void And()
        {
            Assert.Equal(Unlikely, EquallyLikely & EquallyLikely);
            Assert.Equal(0.1875.Chance(), Likely & Unlikely);
            Assert.Equal(Unlikely.And(Likely), Likely & Unlikely);
            Assert.Equal(Likely, Likely & Certain);
            Assert.Equal(Impossible, Impossible & Likely);
        }

        [Fact]
        public void Or()
        {
            Assert.Equal(Likely, EquallyLikely | EquallyLikely);
            Assert.Equal(new Chance(0.8125), Likely | Unlikely);
            Assert.Equal(Unlikely.Or(Likely), Likely | Unlikely);
            Assert.Equal(Certain, Likely | Certain);
            Assert.Equal(Likely, Impossible | Likely);
        }

        [Fact]
        public void InvalidFractions() {
            Assert.Throws<ArgumentException>(() => (-0.1).Chance());
            Assert.Throws<ArgumentException>(() => 1.1.Chance());
        }

        [Fact]
        public void Best() {
            Assert.Null(new List<Chance>(0).GetBest());

            var chances = new List<Chance>(3) {new Chance(0.5), new Chance(0.3), new Chance(0.7)};

            var best = chances.GetBest();
            Assert.Equal(new Chance(0.3), best);
        }
    }
}
