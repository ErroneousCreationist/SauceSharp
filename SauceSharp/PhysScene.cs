using System.Numerics;
using Raylib_cs;
using ChipmunkSharp;

namespace SauceSharp
{
    public class PhysScene : Scene
    {
        public Vector2 Gravity { protected set; get; }
        public cpSpace? PhysicsWorld;
        public float PhysicsTimeStep { get; protected set; }

        public PhysScene(Color bgcol, Action<Scene> InitiateScene, Vector2 gravity, float physframerate = 60) : base(bgcol, InitiateScene)
        {
            Gravity = gravity;
            PhysicsWorld = new cpSpace();
            PhysicsWorld.SetGravity(gravity.Convert());
            PhysicsTimeStep = 1 / physframerate;
        }

        public override void Draw()
        {
            base.Draw();
            if (PhysicsWorld == null) { return; }
            PhysicsWorld.Step(PhysicsTimeStep);
        }
    }
}

