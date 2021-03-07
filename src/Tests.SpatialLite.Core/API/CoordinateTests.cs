
using SpatialLite.Core.API;
using Xunit;

namespace Tests.SpatialLite.Core.API
{
    public class CoordinateTests
    {
        float xCoordinate = 3.5f;
        float yCoordinate = 4.2f;
        float zCoordinate = 10.5f;

        [Fact]
        public void Constructor_XY_SetsXYValuesAndZMNaN()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate);

            Assert.Equal(xCoordinate, target.X);
            Assert.Equal(yCoordinate, target.Y);
            Assert.Equal(double.NaN, target.Z);
        }

        [Fact]
        public void Constructor_XYZ_SetsXYZValuesAndMNaN()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.Equal(xCoordinate, target.X);
            Assert.Equal(yCoordinate, target.Y);
            Assert.Equal(zCoordinate, target.Z);
        }

        [Fact]
        public void Is3D_ReturnsFalseForNaNZCoordinate()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate);

            Assert.False(target.Is3D);
        }

        [Fact]
        public void Is3D_ReturnsTrueFor3D()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.True(target.Is3D);
        }

        [Fact]
        public void Equals_ReturnsTrueForCoordinateWithTheSameOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.True(target.Equals(other));
        }

        [Fact]
        public void Equals_ReturnsTrueForNaNCoordinates()
        {
            Coordinate target = new Coordinate(float.NaN, float.NaN, float.NaN);
            Coordinate other = new Coordinate(float.NaN, float.NaN, float.NaN);

            Assert.True(target.Equals(other));
        }

        [Fact]
        public void Equals_ReturnsFalseForNull()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            object other = null;

            Assert.False(target.Equals(other));
        }

        [Fact]
        public void Equals_ReturnsFalseForOtherObjectType()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            object other = "string";

            Assert.False(target.Equals(other));
        }

        [Fact]
        public void Equals_ReturnsFalseForCoordinateWithDifferentOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate + 1, yCoordinate + 1, zCoordinate + 1);

            Assert.False(target.Equals(other));
        }

        [Fact]
        public void Equals2D_ReturnsTrueForCoordinateWithTheSameOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.True(target.Equals2D(other));
        }

        [Fact]
        public void Equals2D_ReturnsTrueForCoordinateWithTheDifferentZOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate, yCoordinate, zCoordinate + 1);

            Assert.True(target.Equals2D(other));
        }

        [Fact]
        public void Equals2D_ReturnsTrueForNaNCoordinates()
        {
            Coordinate target = new Coordinate(float.NaN, float.NaN, float.NaN);
            Coordinate other = new Coordinate(float.NaN, float.NaN, float.NaN);

            Assert.True(target.Equals2D(other));
        }

        [Fact]
        public void Equals2D_ReturnsFalseForCoordinateWithDifferentXYOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate + 1, yCoordinate + 1, zCoordinate);

            Assert.False(target.Equals2D(other));
        }

        [Fact]
        public void EqualsOperator_ReturnsTrueForCoordinateWithTheSameOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.True(target == other);
        }

        [Fact]
        public void EqualsOperator_ReturnsTrueForNaNCoordinates()
        {
            Coordinate target = new Coordinate(float.NaN, float.NaN, float.NaN);
            Coordinate other = new Coordinate(float.NaN, float.NaN, float.NaN);

            Assert.True(target == other);
        }

        [Fact]
        public void EqualsOperator_ReturnsFalseForCoordinateWithDifferentOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate + 1, yCoordinate + 1, zCoordinate + 1);

            Assert.False(target == other);
        }

        [Fact]
        public void NotEqualsOperator_ReturnsFalseForCoordinateWithTheSameOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate, yCoordinate, zCoordinate);

            Assert.False(target != other);
        }

        [Fact]
        public void NotEqualsOperator_ReturnsFalseForNaNCoordinates()
        {
            Coordinate target = new Coordinate(float.NaN, float.NaN, float.NaN);
            Coordinate other = new Coordinate(float.NaN, float.NaN, float.NaN);

            Assert.False(target != other);
        }

        [Fact]
        public void NotEqualsOperator_ReturnsTrueForCoordinateWithDifferentOrdinates()
        {
            Coordinate target = new Coordinate(xCoordinate, yCoordinate, zCoordinate);
            Coordinate other = new Coordinate(xCoordinate + 1, yCoordinate + 1, zCoordinate + 1);

            Assert.True(target != other);
        }
    }
}
