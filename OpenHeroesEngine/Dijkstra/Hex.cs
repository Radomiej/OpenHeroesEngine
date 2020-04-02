using System;
using System.Collections.Generic;
using System.Numerics;
using OpenHeroesEngine.AStar;
using OpenHeroesEngine.WorldMap.Models;

namespace OpenHeroesEngine.Dijkstra
{
   public class Hex : Node{
    // vectorized cube constructor
    public Hex(Point coords) {
        this.coords = coords;
        // if (coords.X + coords.y + coords.z != 0) throw new ArgumentException("x + y + z must be 0");
    }
    public Hex(int x, int y) : this(new Point(x, y)) {
    }
    
    public readonly Point coords;

    public override bool Equals(object obj) {
        if (!object.ReferenceEquals(obj,null)) {
            return this.Equals((Hex)obj);
        }
        else {
            return false;
        }
    }

    public bool Equals(Hex obj) {
        if (!object.ReferenceEquals(obj, null)) {
            return Equals(this.coords, obj.coords);
        }
        else {
            return false;
        }
    }

    public override int GetHashCode() {
        return (coords.X * 0x100000) + (coords.Y * 0x1000);
    }

    public static bool operator ==(Hex h1, Hex h2) {
        return h1.Equals(h2);
    }

    public static bool operator !=(Hex h1, Hex h2) {
        return !h1.Equals(h2);
    }

    public Hex Add(Hex b) {
        return new Hex(coords: coords + b.coords);
    }

    public Hex Subtract(Hex b) {
        return new Hex(coords - b.coords);
    }

    public int Length() {
        return (int)((Math.Abs(coords.X) + Math.Abs(coords.Y)) / 2);
    }

    public int Distance(Hex b) {
        return Subtract(b).Length();
    }

    static public List<Point> directionOffsets = new List<Point> { new Point(1, 1), new Point(1, 0), new Point(1, -1), new Point(0, 1), new Point(0, -1), new Point(-1, 1), new Point(-1, 0), new Point(-1, -1)};

    static public Point GetDirectionOffset(int direction) {
        return Hex.directionOffsets[direction];
    }

    public Point GetNeighborCoords(int direction) {
        return this.coords + Hex.directionOffsets[direction];
    }

    public override string ToString()
    {
        return $"{nameof(coords)}: {coords}";
    }
   }
}