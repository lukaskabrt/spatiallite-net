using SpatialLite.Core.Api;
using System;
using Tests.SpatialLite.Core.FluentAssertions;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public partial class EnvelopeTests
    {
        /* Extend(Coordinate) */

        [Fact]
        public void Extend_SetsMinMaxValuesOnEmptyEnvelopeForSingleCoordinate()
        {
            var source = Envelope.Empty;
            var coordinate = new Coordinate(1, 2);

            var target = source.Extend(coordinate);

            target.ShouldHaveBounds(coordinate.X, coordinate.X, coordinate.Y, coordinate.Y);
        }

        [Fact]
        public void Extend_DoesNothingIfCoordinateIsEmpty()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(Coordinate.Empty);

            target.ShouldHaveSameBounds(source);
        }

        public static TheoryData<Coordinate, float, float, float, float> CoordinateWithExtendedBounds => new()
        {
            { new Coordinate(10, 0), -1, 10, -2, 2 },
            { new Coordinate(-10, 0), -10, 1, -2, 2 },
            { new Coordinate(0, 10), -1, 1, -2, 10 },
            { new Coordinate(0, -10), -1, 1, -10, 2 }
        };

        [Theory]
        [MemberData(nameof(CoordinateWithExtendedBounds))]
        public void Extend_ExtendsEnvelopeWithSingleCoordinate(Coordinate coordinate, float minX, float maxX, float minY, float maxY)
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(coordinate);

            target.ShouldHaveBounds(minX, maxX, minY, maxY);
        }

        [Fact]
        public void Extend_DoesNothingForSingleCoordinateInsideEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(new Coordinate(0.5f, 0.5f));

            target.ShouldHaveSameBounds(source);
        }

        /* Extend(IReadOnlyList<Coordinate>) */

        [Fact]
        public void Extend_SetsMinMaxValuesOnEmptyEnvelopeForMultipleCoordinates()
        {
            var source = Envelope.Empty;

            var target = source.Extend(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            target.ShouldHaveBounds(-1, 1, -2, 2);
        }

        [Fact]
        public void Extend_DoesNothingForEmptyCollection()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(Array.Empty<Coordinate>());

            target.ShouldHaveSameBounds(source);
        }

        [Fact]
        public void Extend_DoesNothingForCoordinatesInsideEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(new[] { new Coordinate(0.5f, 0.5f), new Coordinate(0.25f, 0.25f) });

            target.ShouldHaveSameBounds(source);
        }

        [Fact]
        public void Extend_Coordinates_ExtendsEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(new[] { new Coordinate(-10, -20), new Coordinate(10, 20) });

            target.ShouldHaveBounds(-10, 10, -20, 20);
        }

        /* Extend(Envelope) */

        [Fact]
        public void Extend_SetsMinMaxValuesOnEmptyEnvelope()
        {
            var source = Envelope.Empty;
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(other);

            target.ShouldHaveSameBounds(other);
        }

        [Fact]
        public void Extend_DoesNothingIfOtherEnvelopeIsEmpty()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var other = Envelope.Empty;

            var target = source.Extend(other);

            target.ShouldHaveSameBounds(source);
        }

        [Fact]
        public void Extend_DoesNothingIfEnvelopeIsInsideTargetEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(10, 20), new Coordinate(-10, -20) });
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            var target = source.Extend(other);

            target.ShouldHaveSameBounds(source);
        }

        [Fact]
        public void Extend_ExtendsEnvelopeWithOtherEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var other = new Envelope(new[] { new Coordinate(10, 20), new Coordinate(-10, -20) });

            var target = source.Extend(other);

            target.ShouldHaveSameBounds(other);
        }
    }
}
