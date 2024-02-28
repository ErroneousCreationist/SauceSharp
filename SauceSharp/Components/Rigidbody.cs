using System;
using ChipmunkSharp;

namespace SauceSharp.Components
{
	public class Rigidbody : Component
	{
        protected RigidbodyDataBase? Data; 
        public cpBody? MyBody { protected set; get; }
        public cpShape? MyShape { protected set; get; }
        public cpVect BaseVelocity;
		public Rigidbody(RigidbodyDataBase rigidbodyData, System.Numerics.Vector2 basevelocity = default) : base()
		{
            Data = rigidbodyData;
            BaseVelocity = basevelocity.Convert();
		}

        protected override void Added()
        {
            if(Data == null) { return; }
            if(Game.TheGame.GetCurrentScene is not PhysScene) { return; }
            cpSpace? w = (Game.TheGame.GetCurrentScene as PhysScene).PhysicsWorld; //ignore this, it has to be a physScene
            if (w == null) { return; }
            if (Owner == null) { return; }
            MyBody = new(Data.Mass, Data.Moment)
            {
                bodyType = Data.Type
            };
            MyBody.SetPosition(Owner.Pos.Convert());
            MyBody.SetAngle(Owner.Rot);
            cpShape? s = null;
            switch (Data.Shape)
            {
                case ColliderShapeEnum.Circle:
                    {
                        if (Data is not CircleRigidbodyData) { return; }
                        cpCircleShape circle = new(MyBody, (Data as CircleRigidbodyData).Radius, Data.Offset.Convert());
                        s = circle;
                    }
                    break;
                case ColliderShapeEnum.Polygon:
                    {
                        if (Data is not PolygonRigidbodyData) { return; }
                        cpVect[] points = new cpVect[(Data as PolygonRigidbodyData).Verts.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            points[i] = (Data as PolygonRigidbodyData).Verts[i].Convert();
                        }
                        cpPolyShape poly = new(MyBody, points.Length, points, 1);
                        s = poly;
                    }
                    break;
                case ColliderShapeEnum.Edge:
                    {
                        if (Data is not EdgeRigidbodyData) { return; }
                        cpSegmentShape edge = new(MyBody, (Data as EdgeRigidbodyData).LocalStartPos.Convert(),  (Data as EdgeRigidbodyData).LocalEndPos.Convert(), 1);
                        s = edge;
                    }
                    break;
                case ColliderShapeEnum.Box:
                    {
                        if (Data is not BoxRigidbodyData) { return; }
                        var data = Data as BoxRigidbodyData;
                        cpPolyShape box = new(MyBody, 4, new cpVect[] { new cpVect(data.Width, -data.Height), new cpVect(data.Width, data.Height), new cpVect(-data.Width, data.Height), new cpVect(-data.Width, -data.Height) }, 10);
                        s = box;
                    }
                    break;
            }
            if (s == null) { return; }
            s.SetFriction(Data.Friction);
            MyBody.AddShape(s);
            w.AddBody(MyBody);
            w.AddShape(s);
            MyBody.SetVelocity(BaseVelocity);
            MyShape = s;
            MyShape.SetElasticity(Data.Elasticity);
        }

        public override void DrawUpdate()
        {
            if(Owner == null) { return; }
            if(MyBody == null) { return; }
            Owner.Pos = MyBody.GetPosition().Convert();
            Owner.Rot = MyBody.GetAngle();
        }
    }
}

