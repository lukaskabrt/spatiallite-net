﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using SpatialLite.Core.Geometries;
using SpatialLite.Core.API;

namespace Tests.SpatialLite.Core.Geometries {
   public class ReadOnlyCoordinateListTests {
       List<Point> _points = new List<Point>(new Point[] { 
			new Point(5, 1.1, 2.2),
			new Point(6, 10.1, -20.2),
			new Point(7, -30.1, 40.2) });


       [Fact]
       public void Constructor_Source_SetsSource() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Same(_points, target.Source);
       }

       [Fact]
       public void Indexer_Get_ReturnsCoordinatesFromSourceList() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           for (int i = 0; i < _points.Count; i++) {
               Assert.Equal(_points[i].Position, target[i]);
           }
       }

       [Fact]
       public void Indexer_Set_ThrowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target[0] = new Coordinate(10.1, 11.2));
       }

       [Fact]
       public void Count_GetsNumberOfItemsInSourceCollection() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Equal(_points.Count, target.Count);
       }

       [Fact]
       public void Add_Coordinate_ThowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target.Add(Coordinate.Empty));
       }

       [Fact]
       public void Add_Coordinates_ThowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target.Add(new Coordinate[] { Coordinate.Empty }));
       }

       [Fact]
       public void Insert_Index_Coordinate_ThowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target.Insert(0, Coordinate.Empty));
       }

       [Fact]
       public void RemoveAt_Index_ThowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target.RemoveAt(0));
       }

       [Fact]
       public void Clear_ThowsNotSupportedException() {
           ReadOnlyCoordinateList<Point> target = new ReadOnlyCoordinateList<Point>(_points);

           Assert.Throws<NotSupportedException>(() => target.Clear());
       }
    }
}
