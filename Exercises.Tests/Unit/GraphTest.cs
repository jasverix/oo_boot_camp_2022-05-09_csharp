/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using Exercises.Graph;
using Xunit;

namespace Exercises.Tests.Unit {
    // Ensures that graph algorithms operate correctly
    public class GraphTest {
        private static readonly Node A = new();
        private static readonly Node B = new();
        private static readonly Node C = new();
        private static readonly Node D = new();
        private static readonly Node E = new();
        private static readonly Node F = new();
        private static readonly Node G = new();

        static GraphTest() {
            B.Cost(5.0).To(A);
            B.Cost(6.0).To(C).Cost(7.0).To(D).Cost(2.0).To(E).Cost(3.0).To(B).Cost(4.0).To(F);
            C.Cost(1.0).To(D);
            C.Cost(8.0).To(E);
        }

        [Fact]
        public void CanReach() {
            Assert.True(A.CanReach(A));
            Assert.True(B.CanReach(A));
            Assert.True(B.CanReach(F));
            Assert.True(B.CanReach(D));
            Assert.True(C.CanReach(F));
            Assert.False(A.CanReach(B));
            Assert.False(G.CanReach(B));
            Assert.False(B.CanReach(G));
        }

        [Fact]
        public void HopCount() {
            Assert.Equal(0, A.HopCount(A));
            Assert.Equal(1, B.HopCount(A));
            Assert.Equal(1, B.HopCount(F));
            Assert.Equal(2, B.HopCount(D));
            Assert.Equal(3, C.HopCount(F));
            Assert.Throws<ArgumentException>(() => A.HopCount(B));
            Assert.Throws<ArgumentException>(() => G.HopCount(B));
            Assert.Throws<ArgumentException>(() => B.HopCount(G));
        }

        [Fact]
        public void Cost() {
            Assert.Equal(4d, B.MinCost(F));
            Assert.Equal(3d, C.MinCost(E));
            Assert.Equal(9d, B.MinCost(E));
            Assert.Equal(7d, B.MinCost(D));
            Assert.Equal(0.0, D.MinCost(D));
            Assert.Equal(11d, D.MinCost(C));
            Assert.Throws<ArgumentException>(() => A.MinCost(B));
            Assert.Throws<ArgumentException>(() => A.MinCost(G));
            Assert.Throws<ArgumentException>(() => G.MinCost(F));
        }
    }
}
