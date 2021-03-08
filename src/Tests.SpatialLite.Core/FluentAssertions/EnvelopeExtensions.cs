using FluentAssertions;
using FluentAssertions.Execution;
using SpatialLite.Core.API;

namespace Tests.SpatialLite.Core.FluentAssertions
{
    public static class EnvelopeExtensions
    {
        public static void ShouldHaveBounds(this Envelope envelope, float minX, float maxX, float minY, float maxY)
        {
            using (new AssertionScope())
            {
                envelope.MinX.Should().Be(minX);
                envelope.MaxX.Should().Be(maxX);
                envelope.MinY.Should().Be(minY);
                envelope.MaxY.Should().Be(maxY);
            }
        }

        public static void ShouldHaveSameBounds(this Envelope envelope, Envelope expected)
        {
            using (new AssertionScope())
            {
                envelope.MinX.Should().Be(expected.MinX);
                envelope.MaxX.Should().Be(expected.MaxX);
                envelope.MinY.Should().Be(expected.MinY);
                envelope.MaxY.Should().Be(expected.MaxY);
            }
        }
    }
}
