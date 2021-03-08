using System;

namespace SpatialLite.Core.API
{
    /// <summary>
    /// Represents a location in the coordinate space.
    /// </summary>
    public struct Coordinate : IEquatable<Coordinate>
    {
        /// <summary>
        /// Represents an empty coordinate.
        /// </summary>
        /// <remarks>
        /// The empty coordinate has all coordinates equal to NaN.
        /// </remarks>
        public static readonly Coordinate Empty = new Coordinate(float.NaN, float.NaN, float.NaN);

        /// <summary>
        /// Gets the X-coordinate.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Gets the Y-coordinate.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Gets the Z-coordinate.
        /// </summary>
        public float Z { get; private set; }

        /// <summary>
        /// Initializes a <see cref="Coordinate"/> with X, Y coordinates.
        /// </summary>
        /// <param name="x">X-coordinate value.</param>
        /// <param name="y">Y-coordinate value.</param>
        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
            Z = float.NaN;
        }

        /// <summary>
        /// Initializes a <see cref="Coordinate"/> with X, Y, Z coordinates.
        /// </summary>
        /// <param name="x">X-coordinate value.</param>
        /// <param name="y">Y-coordinate value.</param>
        /// <param name="z">Z-coordinate value.</param>
        public Coordinate(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Coordinate"/>has assigned <see cref="Z"/> coordinate.
        /// </summary>
        public bool Is3D => !double.IsNaN(this.Z);

        /// <summary>
        /// Gets a value indicating whether this <see cref="Coordinate"/> represents <see cref="Coordinate.Empty"/>.
        /// </summary>
        public bool IsEmpty => float.IsNaN(this.X) || float.IsNaN(this.Y);

        /// <summary>
        /// Determines whether specific <see cref="Coordinate"/> values are equal
        /// </summary>
        /// <param name="lhs">Coordinate to compare</param>
        /// <param name="rhs">Coordinate to compare</param>
        /// <returns>true if the specified <c>Coordinate</c> values are equal; otherwise, false.</returns>
        public static bool operator ==(Coordinate lhs, Coordinate rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether specific <see cref="Coordinate"/> values are not equal
        /// </summary>
        /// <param name="lhs">Coordinate to compare</param>
        /// <param name="rhs">Coordinate to compare</param>
        /// <returns>true if the specified <c>Coordinate</c> values are not equal; otherwise, false.</returns>
        public static bool operator !=(Coordinate lhs, Coordinate rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Returns a <c>string</c> representation of the current <see cref="Coordinate"/>.
        /// </summary>
        /// <returns>A <c>string</c> representation of the current <see cref="Coordinate"/></returns>
        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "[{0}; {1}; {2}]", this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Determines whether specific <c>object</c> instance is equal to the current <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="obj">The <c>object</c> to compare with the current <see cref="Coordinate"/></param>
        /// <returns>true if the specified <c>object</c> is equal to the current <see cref="Coordinate"/> otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Coordinate other)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether two <see cref="Coordinate"/> values are equal.
        /// </summary>
        /// <param name="other">The <see cref="Coordinate"/> to compare with the current <see cref="Coordinate"/></param>
        /// <returns>true if the specified <see cref="Coordinate"/> is equal to the current <see cref="Coordinate"/>; otherwise, false.</returns>
        public bool Equals(Coordinate other)
        {
            return Equals2D(other) && ((this.Z == other.Z) || (double.IsNaN(this.Z) && double.IsNaN(other.Z)));
        }

        /// <summary>
        /// Serves as a hash function for the <see cref="Coordinate"/> structure.
        /// </summary>
        /// <returns>Hash code for current <see cref="Coordinate"/> value.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Determines whether two <see cref="Coordinate"/> are equal in the 2D space.
        /// </summary>
        /// <param name="other">The <see cref="Coordinate"/> to compare with the current <see cref="Coordinate"/></param>
        /// <returns>true if the specified <see cref="Coordinate"/> is equal to the current <see cref="Coordinate"/> in the 2D space otherwise, false.</returns>
        public bool Equals2D(Coordinate other)
        {
            return (this.IsEmpty && other.IsEmpty) || ((this.X == other.X) && (this.Y == other.Y));
        }
    }
}
