using System;
using Raylib_cs;

namespace SauceSharp.Components
{
    public class Graphics : Component
    {
        protected Texture2D Texture;
        protected Color Tint;

        public Graphics(Texture2D texture, Color tint) : base()
        {
            Texture = texture;
            Tint = tint;
        }

        public override void DrawUpdate()
        {
            if (Owner == null) { return; }
            Game.TheGame.Draw_Texture(new System.Numerics.Vector2(Owner.Pos.X+Texture.Width/2, Owner.Pos.Y+Texture.Height/2), Owner.Rot, Owner.Scale.X, Tint, Texture);
        }
    }
}

