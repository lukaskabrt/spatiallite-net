using SpatialLite.Core.Api;
using System.Collections.Generic;

namespace SpatialLite.Core.Geometries
{
    /// <summary>
    /// Represents a curve with linear interpolation between consecutive vertices.  
    /// </summary>
    public class LineString : Geometry, ILineString
    {

        private CoordinateSequence _coordinates;

        /// <summary>
        /// Initializes a new instance of the <c>LineString</c> class that is empty.
        /// </summary>
        public LineString()
            : base()
        {
            _coordinates = new CoordinateSequence();
        }

        /// <summary>
        /// Initializes a new instance of the <c>LineString</c> class with specified coordinates.
        /// </summary>
        /// <param name="coords">The collection of coordinates to be copied to the new LineString.</param>
        public LineString(IEnumerable<Coordinate> coords)
            : base()
        {
            _coordinates = new CoordinateSequence(coords);
        }

        /// <summary>
        /// Gets the list of coordinates that define this LisneString
        /// </summary>
        public virtual ICoordinateSequence Coordinates
        {
            get { return _coordinates; }
        }

        /// <summary>
        /// Gets the list of coordinates that define this LisneString
        /// </summary>
        ICoordinateSequence ILineString.Coordinates
        {
            get { return this.Coordinates; }
        }

        /// <summary>
        /// Gets the first coordinate of the <c>ILineString</c> object.
        /// </summary>
        public Coordinate Start => _coordinates.Count == 0 ? Coordinate.Empty : _coordinates[0];

        /// <summary>
        /// Gets the last coordinate of the <c>ILineString</c> object.
        /// </summary>
        public Coordinate End => _coordinates.Count == 0 ? Coordinate.Empty : _coordinates[_coordinates.Count - 1];

        /// <summary>
        /// Gets a value indicating whether this <c>LineString</c> is closed.
        /// </summary>
        /// <remarks>
        /// The LineStringBase is closed if <see cref="Start"/> and <see cref="End"/> are identical.
        /// </remarks>
        public virtual bool IsClosed => _coordinates.Count == 0 ? false : this.Start.Equals(this.End);

        /// <summary>
        /// Gets collection of all <see cref="Coordinate"/> of this IGeometry object
        /// </summary>
        /// <returns>the collection of all <see cref="Coordinate"/> of this object</returns>
        public override IReadOnlyList<Coordinate> GetCoordinates()
        {
            return _coordinates;
        }
    }
}
