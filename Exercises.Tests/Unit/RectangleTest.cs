/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Exercises.Geometry;
using Exercises.Sorting;
using Xunit;

namespace Exercises.Tests.Unit {
    // Ensures that Rectangle works correctly
    public class RectangleTest {
        [Fact]
        public void Area() {
            Assert.Equal(24.0, new Rectangle(4, 6.0).Area());
        }

        [Fact]
        public void Perimeter() {
            Assert.Equal(20.0, new Rectangle(4.0, 6).Perimeter());
        }

        [Fact]
        public void InvalidRectangles() {
            Assert.Throws<ArgumentException>(() => new Rectangle(0, 6.0));
            Assert.Throws<ArgumentException>(() => new Rectangle(4, 0.0));
        }
    }

    public class SquareTest {
        [Fact]
        public void Equal() {
            Assert.Equal(new Rectangle(4d).Area(), new Rectangle(4).Area());
            Assert.Equal(new Rectangle(4, 4).Area(), new Rectangle(4).Area());
        }

        [Fact]
        public void Area() {
            Assert.Equal(16d, new Rectangle(4).Area());
            Assert.Equal(20.25, new Rectangle(4.5).Area());
        }

        [Fact]
        public void Perimeter() {
            Assert.Equal(24, new Rectangle(6).Perimeter());
            Assert.Equal(18d, new Rectangle(4.5).Perimeter());
        }

        [Fact]
        public void InvalidSquares() {
            Assert.Throws<ArgumentException>(() => new Rectangle(0));
            Assert.Throws<ArgumentException>(() => new Rectangle(-1));
        }

        [Fact]
        public void Best() {
            Assert.Null(new List<Rectangle>(0).GetBest());

            var rectangles = new List<Rectangle>(3) {new Rectangle(2, 3), new Rectangle(5, 6), new Rectangle(1, 2)};

            var largest = rectangles.GetBest();
            Assert.Equal(new Rectangle(5, 6).Area(), largest!.Area());
        }
    }
}
