/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using System;
using Exercises.Sorting;

namespace Exercises.Geometry {
    // Understands a four-sided polygon with sides at right angles
    public class Rectangle: IBetterable {
        private readonly double _length;
        private readonly double _width;

        public Rectangle(double length, double width) {
            if (length <= 0.0 || width <= 0.0) throw new ArgumentException("Invalid dimensions");
            _length = length;
            _width = width;
        }

        public Rectangle(double sideSize) : this(sideSize, sideSize) {
        }

        public double Area() => _length * _width;

        public double Perimeter() => 2 * (_length + _width);

        public override int GetHashCode()
            => HashCode.Combine(_width.GetHashCode(), _length.GetHashCode());

        public bool IsBetter(IBetterable obj)
            => (obj is Rectangle other) && other.Area() < this.Area();

        public bool IsSquare()
            => Math.Abs(_length - _width) < double.Epsilon;

        public override string ToString() => $"{_width}, {_length}";
    }
}
