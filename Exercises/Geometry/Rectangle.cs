/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using Exercises.Order;

namespace Exercises.Geometry;

// Understands a four-sided polygon with sides at right angles-
public class Rectangle : Orderable<Rectangle> {
    private readonly double _length;
    private readonly double _width;

    public Rectangle(double length, double width) {
        if (length <= 0.0 || width <= 0.0) throw new ArgumentException("Invalid dimensions");
        _length = length;
        _width = width;
    }

    public static Rectangle Square(double side) => new Rectangle(side, side);

    public double Area() => _length * _width;

    public double Perimeter() => 2 * (_length + _width);

    public bool IsBetterThan(Rectangle other) => this.Area() > other.Area();
}
