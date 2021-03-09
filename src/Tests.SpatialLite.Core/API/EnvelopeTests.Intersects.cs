using FluentAssertions;
using SpatialLite.Core.Api;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public partial class EnvelopeTests
    {
        /* Intersects(Envelope) */

        public static TheoryData<Envelope> IntersectingEnvelopes => new()
        {
            { new Envelope(new[] { new Coordinate(2, 2), new Coordinate(0, 0) }) },
            { new Envelope(new[] { new Coordinate(2, -2), new Coordinate(0, 0) }) },
            { new Envelope(new[] { new Coordinate(-2, 2), new Coordinate(0, 0) }) },
            { new Envelope(new[] { new Coordinate(-2, -2), new Coordinate(0, 0) }) }
        };

        [Theory]
        [MemberData(nameof(IntersectingEnvelopes))]
        public void Intersects_ReturnsTrueForIntersectingEnvelopes(Envelope other)
        {
            var target = new Envelope(new[] { new Coordinate(1, 1), new Coordinate(-1, -1) });

            target.Intersects(other).Should().BeTrue();
        }

        [Fact]
        public void Intersects_ReturnsTrueIfTargetEnvelopeCoversOtherEnvelope()
        {
            var target = new Envelope(new[] { new Coordinate(10, 20), new Coordinate(-10, -20) });
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            target.Intersects(other).Should().BeTrue();
        }

        [Fact]
        public void Intersects_ReturnsTrueIfOtherEnvelopeCoversTargetEnvelope()
        {
            var target = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var other = new Envelope(new[] { new Coordinate(10, 20), new Coordinate(-10, -20) });

            target.Intersects(other).Should().BeTrue();
        }

        [Fact]
        public void Intersects_ReturnsTrueIfBothEnvelopesAreSame()
        {
            var target = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            target.Intersects(other).Should().BeTrue();
        }

        public static TheoryData<Envelope> TouchingEnvelopes => new()
        {
            { new Envelope(new[] { new Coordinate(2, 2), new Coordinate(1, 1) }) },
            { new Envelope(new[] { new Coordinate(2, -2), new Coordinate(1, -1) }) },
            { new Envelope(new[] { new Coordinate(-2, 2), new Coordinate(-1, 1) }) },
            { new Envelope(new[] { new Coordinate(-2, -2), new Coordinate(-1, -1) }) }
        };

        [Theory]
        [MemberData(nameof(TouchingEnvelopes))]
        public void Intersects_ReturnsTrueForTouchingEnvelopes(Envelope other)
        {
            var target = new Envelope(new[] { new Coordinate(1, 1), new Coordinate(-1, -1) });

            target.Intersects(other).Should().BeTrue();
        }

        [Fact]
        public void Intersects_ReturnsFalseIfTargetEnvelopeIsEmpty()
        {
            var other = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            Envelope.Empty.Intersects(other).Should().BeFalse();
        }

        [Fact]
        public void Intersects_ReturnsFalseIfOtherEnvelopeIsEmpty()
        {
            var target = new Envelope(new[] { new Coordinate(1, 2), new Coordinate(-1, -2) });

            target.Intersects(Envelope.Empty).Should().BeFalse();
        }

        public static TheoryData<Envelope> NonIntersectingEnvelopes => new()
        {
            { new Envelope(new[] { new Coordinate(20, 20), new Coordinate(10, 10) }) },
            { new Envelope(new[] { new Coordinate(20, -20), new Coordinate(10, -10) }) },
            { new Envelope(new[] { new Coordinate(-20, 20), new Coordinate(-10, 10) }) },
            { new Envelope(new[] { new Coordinate(-20, -20), new Coordinate(-10, -10) }) }
        };

        [Theory]
        [MemberData(nameof(NonIntersectingEnvelopes))]
        public void Intersects_ReturnsFalseForNonIntersectingEnvelopes(Envelope other)
        {
            var target = new Envelope(new[] { new Coordinate(1, 1), new Coordinate(-1, -1) });

            target.Intersects(other).Should().BeFalse();
        }
    }
}
