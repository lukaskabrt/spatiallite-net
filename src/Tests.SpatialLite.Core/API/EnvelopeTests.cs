using SpatialLite.Core.API;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public partial class EnvelopeTests
    {

        private readonly Envelope _envelopeS = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
        private readonly Envelope _envelopeL = new Envelope(new[] { new Coordinate(10, 20), new Coordinate(-10, -20) });

        Coordinate[] _coordinates = new Coordinate[] {
                new Coordinate(1, 10, 100),
                new Coordinate(0, 0, 0),
                new Coordinate(-1, -10, -100)
        };

        static public IEnumerable<object[]> _XYZEnvelopeDifferentBounds
        {
            get
            {
                yield return new object[] { new Coordinate[] { new Coordinate(1 + 1, 2, 100), new Coordinate(5, 6, 200) } };
                yield return new object[] { new Coordinate[] { new Coordinate(1, 2 + 1, 100), new Coordinate(5, 6, 200) } };
                yield return new object[] { new Coordinate[] { new Coordinate(1, 2, 100 + 1), new Coordinate(5, 6, 200) } };
                yield return new object[] { new Coordinate[] { new Coordinate(1, 2, 100), new Coordinate(5 + 1, 6, 200) } };
                yield return new object[] { new Coordinate[] { new Coordinate(1, 2, 100), new Coordinate(5, 6 + 1, 200) } };
                yield return new object[] { new Coordinate[] { new Coordinate(1, 2, 100), new Coordinate(5, 6, 200 + 1) } };
            }
        }

        internal void CheckBoundaries(Envelope target, float minX, float maxX, float minY, float maxY)
        {
            Assert.Equal(minX, target.MinX);
            Assert.Equal(maxX, target.MaxX);
            Assert.Equal(minY, target.MinY);
            Assert.Equal(maxY, target.MaxY);
        }

        [Fact]
        public void Extend_Coordinate_SetsMinMaxValuesOnEmptyEnvelope()
        {
            var source = Envelope.Empty;
            var coordinate = new Coordinate(1, 2);

            var target = source.Extend(coordinate);

            CheckBoundaries(target, coordinate.X, coordinate.X, coordinate.Y, coordinate.Y);
        }

        [Fact]
        public void Extend_Coordinate_DoesNothingIfCoordinateIsEmpty()
        {
            var target = _envelopeS;

            target.Extend(Coordinate.Empty);

            CheckBoundaries(target, _envelopeS.MinX, _envelopeS.MaxX, _envelopeS.MinY, _envelopeS.MaxY);
        }

        [Theory]
        [InlineData(10, 0, -1, 10, -2, 2)]
        [InlineData(-10, 0, -10, 1, -2, 2)]
        [InlineData(0, 10, -1, 1, -2, 10)]
        [InlineData(0, -10, -1, 1, -10, 2)]
        public void Extend_Coordinate_ExtendsEnvelope(float x1, float y1, float minX, float maxX, float minY, float maxY)
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(new Coordinate(x1, y1));

            CheckBoundaries(target, minX, maxX, minY, maxY);
        }

        [Fact]
        public void Extend_Coordinate_DoosNothingForCoordinateInsideEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(new Coordinate(0.5f, 0.5f));

            CheckBoundaries(target, source.MinX, source.MaxX, source.MinY, source.MaxY);
        }

        [Fact]
        public void Extend_Coordinates_SetsMinMaxValuesOnEmptyEnvelope()
        {
            var source = Envelope.Empty;

            var target = source.Extend(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            CheckBoundaries(target, -1, 1, -2, 2);
        }

        [Fact]
        public void Extend_Coordinates_DoNothingForEmptyCollection()
        {
            var source = _envelopeS;

            var target = source.Extend(Array.Empty<Coordinate>());

            CheckBoundaries(target, _envelopeS.MinX, _envelopeS.MaxX, _envelopeS.MinY, _envelopeS.MaxY);
        }

        [Fact]
        public void Extend_Coordinates_ExtendsEnvelope()
        {
            var source = new Envelope(_coordinates);

            var target = source.Extend(new[] { new Coordinate(-10, -20), new Coordinate(10, 20) });

            CheckBoundaries(target, -10, 10, -20, 20);
        }

        [Fact]
        public void Extend_Envelope_SetsMinMaxValuesOnEmptyEnvelope()
        {
            var source = Envelope.Empty;

            var target = source.Extend(new Envelope(_envelopeS));

            CheckBoundaries(target, _envelopeS.MinX, _envelopeS.MaxX, _envelopeS.MinY, _envelopeS.MaxY);
        }

        [Fact]
        public void Extend_Envelope_DoNothingIfEnvelopeIsInsideTargetEnvelope()
        {
            var source = _envelopeL;

            var target = source.Extend(_envelopeS);

            CheckBoundaries(target, _envelopeL.MinX, _envelopeL.MaxX, _envelopeL.MinY, _envelopeL.MaxY);
        }

        [Fact]
        public void Extend_Envelope_ExtendsEnvelope()
        {
            var source = _envelopeS;
            var other = new Envelope(new[] { new Coordinate(0, 0), new Coordinate(3, 4) });

            var target = source.Extend(other);

            CheckBoundaries(target, _envelopeS.MinX, 3, _envelopeS.MinY, 4);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameObjectInstance()
        {
            object target = new Envelope(_coordinates);

            Assert.True(target.Equals(target));
        }

        [Fact]
        public void Equals_ReturnsTrueForTheEnvelopeWithTheSameBounds()
        {
            var target = new Envelope(_envelopeS);

            Assert.True(target.Equals(_envelopeS));
        }

        [Fact]
        public void Equals_ReturnsFalseForNull()
        {
            var target = new Envelope(_coordinates);
            object other = null;

            Assert.False(target.Equals(other));
        }

        [Fact]
        public void Equals_ReturnsFalseForOtherObjectType()
        {
            var target = new Envelope(_coordinates);
            object other = "string";

            Assert.False(target.Equals(other));
        }

        [Theory]
        [MemberData(nameof(_XYZEnvelopeDifferentBounds))]
        public void Equals_ReturnsFalseForTheEnvelopeWithDifferentBounds(Coordinate[] corners)
        {
            var target = _envelopeS;

            Assert.False(target.Equals(_envelopeL));
        }
    }
}
