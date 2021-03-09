using System.Collections;
using System.Collections.Generic;

using SpatialLite.Core.Api;

namespace SpatialLite.Core.Geometries
{
    /// <summary>
    /// Represents read-write list of Coordinates.
    /// </summary>
    public class CoordinateSequence : ICoordinateSequence
    {

        private List<Coordinate> _storage;

        /// <summary>
        /// Initializes a new instance of the CoordinateList class, that is empty.
        /// </summary>
        public CoordinateSequence()
        {
            _storage = new List<Coordinate>();
        }

        /// <summary>
        /// Initializes a new instance of the CoordinateList class that contains coordinates from the given collection.
        /// </summary>
        /// <param name="coords">The collection whose elements are used to fill CoordinateList.</param>
        public CoordinateSequence(IEnumerable<Coordinate> coords)
        {
            _storage = new List<Coordinate>(coords);
        }

        /// <summary>
        /// Gets number of Coordinates in the list.
        /// </summary>
        public int Count => _storage.Count;

        /// <summary>
        /// Gets or sets Coordinate at the given index.
        /// </summary>
        /// <param name="index">The zero-based index of the Coordinate to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public Coordinate this[int index]
        {
            get => _storage[index];
        }

        /// <summary>
        /// Returns an enumerator that iterates through the CoordinateList
        /// </summary>
        /// <returns>The Enumerator for the CoordinateList</returns>
        public IEnumerator<Coordinate> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the CoordinateList
        /// </summary>
        /// <returns>The Enumerator for the CoordinateList</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_storage).GetEnumerator();
        }
    }
}
