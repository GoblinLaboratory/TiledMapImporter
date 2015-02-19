﻿using Microsoft.Xna.Framework;
using System;

namespace MapImporter
{
    /// <summary>
    /// A polyline follows the same placement definition as a polygon object.
    /// </summary>
    public class Polyline
    {
        /// <summary>
        /// The points that make up the polyline object
        /// </summary>
        public Point[] Points { set; get; }
        /// <summary>
        /// The lines that make up the polyline object
        /// </summary>
        public Line[] Lines { set; get; }
        /// <summary>
        /// Bounding rectangle of this polyline
        /// </summary>
        public Rectangle Bounds { set; get; }
    }

    /// <summary>
    /// A line object that is drawn based on specified points
    /// </summary>
    public struct Line
    {
        /// <summary>
        /// The starting point of the line
        /// </summary>
        public Vector2 Start;
        /// <summary>
        /// The ending point of the line
        /// </summary>
        public Vector2 End;
        /// <summary>
        /// Length of the line
        /// </summary>
        public float Length;
        /// <summary>
        /// Rotation of the line
        /// </summary>
        public float Angle;

        /// <summary>
        /// Create a line from start and end points and calculate the length and angle
        /// </summary>
        /// <param name="start">The first point of the line</param>
        /// <param name="end">The end of the line</param>
        /// <returns>A Line created from the points</returns>
        public static Line FromPoints(Vector2 start, Vector2 end)
        {
            Line l = new Line();
            l.Start = start;
            l.End = end;
            l.Length = Convert.ToSingle(Math.Sqrt(Math.Pow(Math.Abs(start.X - end.X), 2) + Math.Pow(Math.Abs(start.Y - end.Y), 2)));
            l.Angle = Convert.ToSingle(Math.Atan2(end.Y - start.Y, end.X - start.X));
            return l;
        }
    }
}
