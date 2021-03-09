using System;
using System.Collections.Generic;

namespace SpatialLite.Core.Api
{
    /// <summary>
    /// Represents minimal bounding box of a set of coordinates.
    /// </summary>
    public struct Envelope
    {
        /// <summary>
        /// Empty Envelope.
        /// </summary>
        /// <remarks>
        /// Empty envelope has its min values greater that max values.
        /// </remarks>
        public static readonly Envelope Empty = new Envelope(0, -1, 0, -1);

        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;
        private readonly float _maxY;

        /// <summary>
        /// Gets Envelope's minimal x-coordinate.
        /// </summary>
        public float MinX => _minX;

        /// <summary>
        /// Gets Envelope's maximal x-coordinate.
        /// </summary>
        public float MaxX => _maxX;

        /// <summary>
        /// Gets Envelope's minimal y-coordinate.
        /// </summary>
        public float MinY => _minY;

        /// <summary>
        /// Gets Envelope's maximal y-coordinate.
        /// </summary>
        public float MaxY => _maxY;

        /// <summary>
        /// Returns the difference between the maximum and minimum X coordinates.
        /// </summary>
        /// <returns>Width of the envelope.</returns>
        public float Width => this.IsEmpty ? 0 : _maxX - _minX;

        /// <summary>
        /// Returns the difference between the maximum and minimum y coordinates.
        /// </summary>
        /// <returns>Height of the envelope.</returns>
        public float Height => this.IsEmpty ? 0 : _maxY - _minY;

        /// <summary>
        /// Checks if this Envelope equals the empty Envelope.
        /// </summary>
        public bool IsEmpty => _minX > _maxX;

        /// <summary>
        /// Initializes an <see cref="Envelope"/> with the single coordinate.
        /// </summary>
        /// <param name="coord">The coordinate used initialize <see cref="Envelope"/></param>
        public Envelope(Coordinate coord)
        {
            if (coord.IsEmpty)
            {
                _minX = _minY = 0;
                _maxX = _maxY = -1;
                return;
            }

            _minX = coord.X;
            _maxX = coord.X;
            _minY = coord.Y;
            _maxY = coord.Y;
        }

        /// <summary>
        /// Initializes an <see cref="Envelope"/> as copy of the specified envelope.
        /// </summary>
        /// <param name="source">The envelope to be copied.</param>
        public Envelope(Envelope source)
        {
            _minX = source.MinX;
            _maxX = source.MaxX;
            _minY = source.MinY;
            _maxY = source.MaxY;
        }

        /// <summary>
        /// Initializes an <see cref="Envelope"/> that covers specified coordinates.
        /// </summary>
        /// <param name="coords">The coordinates to be covered.</param>
        public Envelope(IReadOnlyList<Coordinate> coords)
        {
            _minX = _minY = 0;
            _maxX = _maxY = -1;

            for (int i = 0; i < coords.Count; i++)
            {
                if (coords[i].IsEmpty) continue;

                if (this.IsEmpty)
                {
                    _minX = coords[i].X;
                    _maxX = coords[i].X;
                    _minY = coords[i].Y;
                    _maxY = coords[i].Y;
                }
                else
                {
                    _minX = _minX < coords[i].X ? _minX : coords[i].X;
                    _maxX = _maxX > coords[i].X ? _maxX : coords[i].X;
                    _minY = _minY < coords[i].Y ? _minY : coords[i].Y;
                    _maxY = _maxY > coords[i].Y ? _maxY : coords[i].Y;
                }
            }
        }

        /// <summary>
        /// Initializes an <see cref="Envelope"/> with specified values.
        /// </summary>
        /// <param name="minX">Minimal X ordinate.</param>
        /// <param name="maxX">Maximal X ordinate.</param>
        /// <param name="minY">Minimal Y ordinate.</param>
        /// <param name="maxY">Maximal Y ordinate.</param>
        private Envelope(float minX, float maxX, float minY, float maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }


        /// <summary>
        /// Extends this <c>Envelope</c> to cover specified <c>Coordinate</c>.
        /// </summary>
        /// <param name="coord">The <c>Coordinate</c> to be covered by extended Envelope.</param>
        public Envelope Extend(Coordinate coord)
        {
            if (this.Covers(coord) || coord.IsEmpty) return this;

            if (this.IsEmpty) return new Envelope(coord);

            return new Envelope(
                _minX < coord.X ? _minX : coord.X,
                _maxX > coord.X ? _maxX : coord.X,
                _minY < coord.Y ? _minY : coord.Y,
                _maxY > coord.Y ? _maxY : coord.Y
           );
        }

        /// <summary>
        /// Extends this <c>Envelope</c> to cover specified <c>Coordinates</c>.
        /// </summary>
        /// <param name="coords">The collection of Coordinates to be covered by extended Envelope.</param>
        public Envelope Extend(IReadOnlyList<Coordinate> coords)
        {
            var envelop = new Envelope(coords);
            return Extend(envelop);
        }

        /// <summary>
        /// Extends this <c>Envelope</c> to cover specified <c>Envelope</c>.
        /// </summary>
        /// <param name="envelope">The <c>Envelope</c> to be covered by extended Envelope.</param>
        public Envelope Extend(Envelope envelope)
        {
            if (this.Covers(envelope) || envelope.IsEmpty) return this;

            return new Envelope(
                _minX < envelope._minX ? _minX : envelope._minX,
                _maxX > envelope._maxX ? _maxX : envelope._maxX,
                _minY < envelope._minY ? _minY : envelope._minY,
                _maxY > envelope._maxY ? _maxY : envelope._maxY
           );
        }

        /// <summary>
        /// Check if the region defined by <c>other</c>
        /// overlaps (intersects) the region of this <c>Envelope</c>.
        /// </summary>
        /// <param name="other"> the <c>Envelope</c> which this <c>Envelope</c> is
        /// being checked for overlapping.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <c>Envelope</c>s overlap.
        /// </returns>
        public bool Intersects(Envelope other)
        {
            if (this.IsEmpty || other.IsEmpty) return false;

            return !(other.MinX > this.MaxX || other.MaxX < this.MinX || other.MinY > this.MaxY || other.MaxY < other.MinY);
        }

        ///<summary>
        /// Tests whether the given <see cref="Coordinate"/> lies in or on the envelope.
        ///</summary>
        /// <param name="coordinate">the <see cref="Coordinate"/> to check.</param>
        /// <returns>True if the <paramref name="coordinate"/> lies in the interior or on the boundary of this envelope.</returns>
        public bool Covers(Coordinate coordinate)
        {
            if (this.IsEmpty) return false;

            return
                coordinate.X >= this.MinX &&
                coordinate.X <= this.MaxX &&
                coordinate.Y >= this.MinY &&
                coordinate.Y <= this.MaxY;
        }

        ///<summary>
        /// Tests if an <see cref="Envelope"/> lies inside this envelope (inclusive of the boundary).
        ///</summary>
        /// <param name="other">The <c>Envelope</c> to check.</param>
        /// <returns>True if this envelope covers the <paramref name="other"/></returns>
        public bool Covers(Envelope other)
        {
            if (this.IsEmpty || other.IsEmpty) return false;

            return
                other.MinX >= this.MinX &&
                other.MaxX <= this.MaxX &&
                other.MinY >= this.MinY &&
                other.MaxY <= this.MaxY;
        }

        /// <summary>
        /// Expands <see cref="Envelope"/> on every side by the specified distance.
        /// </summary>
        /// <param name="dx">Distance in the X-axis direction.</param>
        /// <param name="dy">Distance in the Y-axis direction.</param>
        /// <returns>Expanded <see cref="Envelope"/>.</returns>
        /// <remarks>Both positive and negative values for <paramref name="dx"/> and <paramref name="dy"/> are allowed.</remarks>
        public Envelope Expand(float dx, float dy)
        {
            if (this.IsEmpty) return Envelope.Empty;

            return new Envelope(_minX - dx, _maxX + dx, _minY - dy, _maxY + dy);
        }

        /// <summary>
        /// Determines whether specific <c>object</c> instance is equal to the current <see cref="Envelope"/>.
        /// </summary>
        /// <param name="obj">The <c>object</c> to compare with the current Envelope</param>
        /// <returns>True if the specified <c>object</c> is equal to the current <c>Envelope</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Envelope other)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether two <see cref="Envelope"/> instances are equal.
        /// </summary>
        /// <param name="other">The Envelope to compare with the current Envelope</param>
        /// <returns>True if the specified <c>Envelope</c> is equal to the current <c>Envelope</c>.</returns>
        public bool Equals(Envelope other)
        {
            return
                (this.MinX == other.MinX) &&
                (this.MinY == other.MinY) &&
                (this.MaxX == other.MaxX) &&
                (this.MaxY == other.MaxY);
        }

        /// <summary>
        /// Serves as a hash function for the <see cref="Envelope"/> class.
        /// </summary>
        /// <returns>Hash code for current Envelope object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_minX, _maxX, _minY, _maxY);
        }

        /// <summary>
        /// Determines whether specific <see cref="Envelope"/> values are equal.
        /// </summary>
        /// <param name="lhs">Envelope to compare</param>
        /// <param name="rhs">Envelope to compare</param>
        /// <returns>True if the specified <c>Envelope</c> values are equal.</returns>
        public static bool operator ==(Envelope lhs, Envelope rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether specific <see cref="Envelope"/> values are not equal.
        /// </summary>
        /// <param name="lhs">Envelope to compare</param>
        /// <param name="rhs">Envelope to compare</param>
        /// <returns>True if the specified <c>Envelope</c> values are not equal.</returns>
        public static bool operator !=(Envelope lhs, Envelope rhs)
        {
            return !(lhs == rhs);
        }
    }
}