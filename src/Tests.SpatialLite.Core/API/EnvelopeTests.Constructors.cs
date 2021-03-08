using FluentAssertions;
using SpatialLite.Core.API;
using Tests.SpatialLite.Core.FluentAssertions;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public partial class EnvelopeTests
    {
        /* Envelope(Coordinate) */

        [Fact]
        public void InitializesEmptyEnvelopeForEmptyCoordinate()
        {
            var coordinate = Coordinate.Empty;

            var target = new Envelope(coordinate);

            target.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void InitializesXYProperties()
        {
            var coordinate = new Coordinate(1, 2);

            var target = new Envelope(coordinate);

            target.ShouldHaveBounds(coordinate.X, coordinate.X, coordinate.Y, coordinate.Y);
        }

        /* Envelope(Envelope) */

        [Fact]
        public void CopiesEmptySourceEnvelope()
        {
            var target = new Envelope(Envelope.Empty);

            target.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void CopiesMinMaxValuesFromSourceEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var target = new Envelope(source);

            target.ShouldHaveSameBounds(source);
        }

        /* Envelope(IReadOnlyList<Coordinate>) */

        [Fact]
        public void InitializesEmptyEnvelopeForEmptyList()
        {
            var target = new Envelope(new Coordinate[] { });

            target.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void InitializesEmptyEnvelopeForListOfEmptyCoordinates()
        {
            var target = new Envelope(new[] { Coordinate.Empty, Coordinate.Empty });

            target.IsEmpty.Should().BeTrue();
        }

        public static TheoryData<Coordinate[], float, float, float, float> CoordinatesListWithBounds => new()
        {
            { new[] { new Coordinate(-1, 1), Coordinate.Empty }, -1, -1, 1, 1 },
            { new[] { Coordinate.Empty, new Coordinate(-1, 1) }, -1, -1, 1, 1 },
            { new[] { new Coordinate(-1, 1), new Coordinate(0, 0) }, -1, 0, 0, 1 },
            { new[] { new Coordinate(0, 1), new Coordinate(1, 0) }, 0, 1, 0, 1 },
            { new[] { new Coordinate(0, 0), new Coordinate(1, -1) }, 0, 1, -1, 0 },
            { new[] { new Coordinate(-1, 0), new Coordinate(0, -1) }, -1, 0, -1, 0 }
        };

        [Theory]
        [MemberData(nameof(CoordinatesListWithBounds))]
        public void InitializesEnvelopeWithBoundsFromCoordinates(Coordinate[] coordinates, float minX, float maxX, float minY, float maxY)
        {
            var target = new Envelope(coordinates);

            target.ShouldHaveBounds(minX, maxX, minY, maxY);
        }
    }
}
