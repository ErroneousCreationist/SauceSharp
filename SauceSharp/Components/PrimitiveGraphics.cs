using System.Numerics;
namespace SauceSharp.Components
{
    public class PrimitiveGraphics : Component
    {
        protected PrimitiveType? PrimType;
        public PrimitiveGraphics(PrimitiveType type) : base()
        {
            PrimType = type;
        }

        public override void DrawUpdate()
        {
            if (Owner == null) { return; }
            if(PrimType == null) { return; }
            switch(PrimType.Type)
            {
                case PrimitiveTypeEnum.Circle:
                    if(PrimType is not Primitive_Circle) { return; }
                    if((PrimType as Primitive_Circle).Hollow)
                    {
                        Game.TheGame.Draw_Circle_Outline(Owner.Pos, (PrimType as Primitive_Circle).Radius, (PrimType as Primitive_Circle).Colour);
                    }
                    else
                    {
                        Game.TheGame.Draw_Circle(Owner.Pos, (PrimType as Primitive_Circle).Radius, (PrimType as Primitive_Circle).Colour);
                    }
                    break;
                case PrimitiveTypeEnum.Rectangle:
                    if (PrimType is Primitive_Rectangle)
                    {
                        Game.TheGame.Draw_Rectangle(new Vector2(Owner.Pos.X- (PrimType as Primitive_Rectangle).Size.X/2, Owner.Pos.Y- (PrimType as Primitive_Rectangle).Size.Y/2), Owner.Rot, (PrimType as Primitive_Rectangle).Size, PrimType.Colour);
                    }
                    break;
                case PrimitiveTypeEnum.Sector:
                    if (PrimType is not Primitive_Circle) { return; }
                    if ((PrimType as Primitive_Sector).Hollow)
                    {
                        Game.TheGame.Draw_Circle_Sector_Outline(Owner.Pos, (PrimType as Primitive_Sector).Radius, (PrimType as Primitive_Sector).Startangle, (PrimType as Primitive_Sector).Endangle, PrimType.Colour);
                    }
                    else
                    {
                        Game.TheGame.Draw_Circle_Sector(Owner.Pos, (PrimType as Primitive_Sector).Radius, (PrimType as Primitive_Sector).Startangle, (PrimType as Primitive_Sector).Endangle, PrimType.Colour);
                    }
                    break;
                case PrimitiveTypeEnum.Polygon:
                    if (PrimType is not Primitive_Polygon) { return; }
                    if ((PrimType as Primitive_Polygon).Hollow)
                    {
                        Game.TheGame.Draw_Poly_Outline(Owner.Pos, Owner.Rot, (PrimType as Primitive_Polygon).Sides, (PrimType as Primitive_Polygon).Radius, (PrimType as Primitive_Polygon).Thickness, PrimType.Colour);
                    }
                    else
                    {
                        Game.TheGame.Draw_Poly(Owner.Pos, Owner.Rot, (PrimType as Primitive_Polygon).Sides, (PrimType as Primitive_Polygon).Radius, PrimType.Colour);
                    }
                    break;
            }
        }
    }
}

