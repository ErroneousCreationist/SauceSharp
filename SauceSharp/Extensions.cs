using System;

namespace SauceSharp
{
    #region Primitive Types
    public class PrimitiveType { public PrimitiveTypeEnum Type; public Raylib_cs.Color Colour; public PrimitiveType(PrimitiveTypeEnum type, Raylib_cs.Color col) { Type = type; Colour = col; } }

    public class Primitive_Circle : PrimitiveType
    {
        public float Radius, Thickness;
        public bool Hollow;

        /// <summary>
        /// Describes a circle
        /// </summary>
        public Primitive_Circle(float radius, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Circle, col) { Radius = radius; Thickness = 0; Hollow = false; }

        /// <summary>
        /// Describes a hollow circle
        /// </summary>
        public Primitive_Circle(float radius, float thickness, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Circle, col) { Radius = radius; Thickness = thickness; Hollow = true; }
    }

    public class Primitive_Rectangle : PrimitiveType
    {
        public System.Numerics.Vector2 Size;

        /// <summary>
        /// Describes a rectangle 
        /// </summary>
        public Primitive_Rectangle(System.Numerics.Vector2 size, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Rectangle, col) { Size = size; }
    }

    public class Primitive_Sector : PrimitiveType
    {
        public float Radius, Startangle, Endangle;
        public bool Hollow;

        /// <summary>
        /// Describes a sector of a circle
        /// </summary>
        public Primitive_Sector(float radius, float startangle, float endangle, bool hollow, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Sector, col) { Radius = radius; Startangle = startangle; Hollow = hollow; Endangle = endangle; }
    }

    public class Primitive_Polygon : PrimitiveType
    {
        public float Radius, Thickness;
        public int Sides;
        public bool Hollow;

        /// <summary>
        /// Describes a regular polygon
        /// </summary>
        public Primitive_Polygon(float radius, int sides, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Polygon, col) { Radius = radius; Sides = sides; Hollow = false; Thickness = 0; }

        /// <summary>
        /// Describes the outline of a regular polygon
        /// </summary>
        public Primitive_Polygon(float radius, int sides, float thickness, Raylib_cs.Color col) : base(PrimitiveTypeEnum.Polygon, col) { Radius = radius; Sides = sides; Hollow = true; Thickness = thickness; }
    }

    public enum PrimitiveTypeEnum { Rectangle, Circle, Sector, Polygon }
    #endregion

    #region Rigidbody Data
    public abstract class RigidbodyDataBase {
        public float Mass;
        public ChipmunkSharp.cpBodyType Type;
        public float Moment;
        public float Friction;
        public float Elasticity;
        public System.Numerics.Vector2 Offset;
        public ColliderShapeEnum Shape;
        public RigidbodyDataBase(ColliderShapeEnum shape, ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, System.Numerics.Vector2 offset) { Shape = shape; Mass = mass; Type = type; Moment = moment; Friction = friction; Offset = offset; Elasticity = elasticity; }
    }

    public class BoxRigidbodyData : RigidbodyDataBase
    {
        public float Width, Height;
        /// <summary>
        /// NOTE: WIDTH AND HEIGHT ARE HALF EXTENTS!
        /// </summary>
        public BoxRigidbodyData(ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, float width, float height, System.Numerics.Vector2 offset = default) : base(ColliderShapeEnum.Box,type, mass, moment, friction, elasticity, offset) { Width = width; Height = height; }
    }

    public class CircleRigidbodyData : RigidbodyDataBase
    {
        public float Radius;
        public CircleRigidbodyData(ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, float radius, System.Numerics.Vector2 offset = default) : base(ColliderShapeEnum.Circle, type, mass, moment, friction, elasticity, offset) { Radius = radius; }
    }

    public class EdgeRigidbodyData : RigidbodyDataBase
    {
        public System.Numerics.Vector2 LocalStartPos, LocalEndPos;
        public EdgeRigidbodyData(ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, System.Numerics.Vector2 localstart, System.Numerics.Vector2 localend, System.Numerics.Vector2 offset = default) : base(ColliderShapeEnum.Edge, type, mass, moment, friction, elasticity,offset) { LocalStartPos = localstart; LocalEndPos = localend; }
    }

    public class PolygonRigidbodyData : RigidbodyDataBase
    {
        public System.Numerics.Vector2[] Verts;
        public PolygonRigidbodyData(ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, List<System.Numerics.Vector2> vertices, System.Numerics.Vector2 offset = default) : base(ColliderShapeEnum.Edge, type, mass, moment, friction, elasticity, offset) { Verts = vertices.ToArray(); }
        public PolygonRigidbodyData(ChipmunkSharp.cpBodyType type, float mass, float moment, float friction, float elasticity, System.Numerics.Vector2[] vertices, System.Numerics.Vector2 offset = default) : base(ColliderShapeEnum.Edge, type, mass, moment, friction, elasticity, offset) { Verts = vertices; }
    }

    public enum ColliderShapeEnum { Circle, Edge, Polygon, Box }
    #endregion

    public enum DrawLayer { Bottom, Middle, Top, UI }
	public static class Extensions
	{
        public const float RAD2DEG = 57.2957795131f;
        public const float DEG2RAD = 0.01745329251f;

        public static ChipmunkSharp.cpVect Convert(this System.Numerics.Vector2 vec)
		{
			return new ChipmunkSharp.cpVect(vec.X, vec.Y);
		}
        public static System.Numerics.Vector2 Convert(this ChipmunkSharp.cpVect vec)
        {
            return new System.Numerics.Vector2(vec.x, vec.y);
        }
    }
}

