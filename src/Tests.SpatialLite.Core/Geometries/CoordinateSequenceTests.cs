
using FluentAssertions;
using SpatialLite.Core.Api;
using SpatialLite.Core.Geometries;
using Xunit;

namespace Tests.SpatialLite.Core.Geometries
{
    public class CoordinateSequenceTests
    {
        Coordinate[] _coordinates = new[] {
                new Coordinate(12,10,100),
                new Coordinate(22,20,200),
                new Coordinate(32,30,300)
        };

        [Fact]
        public void Constructor_CreatesEmptyList()
        {
            var target = new CoordinateSequence();

            target.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_CreatesSequenceWithSpecifiedItems()
        {
            var target = new CoordinateSequence(_coordinates);

            target.Should().BeEquivalentTo(_coordinates);
        }

        [Fact]
        public void Indexer_GetsValue()
        {
            var target = new CoordinateSequence(_coordinates);

            target[0].Should().Equals(_coordinates[0]);
        }

        [Fact]
        public void Count_Returns0ForEmptyList()
        {
            var target = new CoordinateSequence();

            target.Count.Should().Be(0);
        }

        [Fact]
        public void Count_ReturnsNumberOfCoordinates()
        {
            var target = new CoordinateSequence(_coordinates);

            target.Count.Should().Be(_coordinates.Length);
        }
    }
}
