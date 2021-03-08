using FluentAssertions;
using SpatialLite.Core.API;
using Tests.SpatialLite.Core.FluentAssertions;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public partial class EnvelopeTests
    {
        /* Height */

        [Fact]
        public void Height_ReturnsZeroForEmptyEnvelope()
        {
            var target = Envelope.Empty;

            target.Height.Should().Be(0);
        }

        [Fact]
        public void Height_ReturnsYDifference()
        {
            var target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            target.Height.Should().Be(2);
        }

        /* Width */

        [Fact]
        public void Width_ReturnsZeroForEmptyEnvelope()
        {
            var target = Envelope.Empty;

            target.Width.Should().Be(0);
        }

        [Fact]
        public void Width_ReturnsXDifference()
        {
            var target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            target.Width.Should().Be(2);
        }

        /* Equals */

        [Fact]
        public void Equals_ReturnsTrueForSameObjectInstance()
        {
            object target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            target.Equals(target).Should().BeTrue();
        }

        [Fact]
        public void Equals_ReturnsTrueForEnvelopeWithTheSameBounds()
        {
            var target = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            target.Equals(other).Should().BeTrue();
        }

        [Fact]
        public void Equals_ReturnsFalseForNull()
        {
            var target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });
            object other = null;

            target.Equals(other).Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsFalseForOtherObjectType()
        {
            var target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });
            object other = "string";

            target.Equals(other).Should().BeFalse();
        }

        public static TheoryData<Envelope> EnvelopesWithOtherBounds => new()
        {
            { new Envelope(new[] { new Coordinate(-2, -1), new Coordinate(1, 1) }) },
            { new Envelope(new[] { new Coordinate(-1, -2), new Coordinate(1, 1) }) },
            { new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(2, 1) }) },
            { new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 2) }) }
        };

        [Theory]
        [MemberData(nameof(EnvelopesWithOtherBounds))]
        public void Equals_ReturnsFalseForEnvelopeWithDifferentBounds(Envelope other)
        {
            var target = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            target.Equals(other).Should().BeFalse();
        }

        /* Expand(float, float) */

        [Fact]
        public void Expand_ExpandsEnvelopeBySpecifiedValues()
        {
            var source = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            source.Expand(1, 2).ShouldHaveBounds(-2, 2, -3, 3);
        }

        [Fact]
        public void Expand_ShrinksEnvelopeBySpecifiedNegativeValues()
        {
            var source = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            source.Expand(-0.5f, -0.1f).ShouldHaveBounds(-0.5f, 0.5f, -0.9f, 0.9f);
        }

        [Fact]
        public void Expand_ShrinksEnvelopeToSinglePoint()
        {
            var source = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            source.Expand(-1, -1).ShouldHaveBounds(0, 0, 0, 0);
        }

        [Fact]
        public void Expand_ShrinksEnvelopeToEmptyEnvelope()
        {
            var source = new Envelope(new[] { new Coordinate(-1, -1), new Coordinate(1, 1) });

            source.Expand(-3, -3).IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void Expand_DoesNotExpandEmptyEnvelope()
        {
            Envelope.Empty.Expand(2, 2).IsEmpty.Should().BeTrue();
        }
    }
}
