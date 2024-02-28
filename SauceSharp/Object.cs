using System;
using System.Numerics;
using Raylib_cs;
using SauceSharp.Components;
namespace SauceSharp
{
	public class Object
	{
		public Vector2 Pos;
        /// <summary>
        /// In radians, by the way
        /// </summary>
		public float Rot;
		public Vector2 Scale;
        protected DrawLayer layer;
        public float RotInDegrees
        {
            get { return Rot * Extensions.RAD2DEG; }
        }

        protected List<Component> Components;

		public Object(Vector2 pos, float rotation, Vector2 scale, DrawLayer layer, Scene thescene)
		{
			Pos = pos;
			Rot = rotation;
			Scale = scale;
            this.layer = layer;
            Components = new List<Component>();

            switch (layer)
            {
                case DrawLayer.Bottom:
                    thescene.OnDrawEarly += preDrawUpdate;
                    break;
                case DrawLayer.Middle:
                    thescene.OnDraw += preDrawUpdate;
                    break;
                case DrawLayer.Top:
                    thescene.OnDrawLate += preDrawUpdate;
                    break;
                case DrawLayer.UI:
                    thescene.OnDrawUI += preDrawUpdate;
                    break;
            }
        }

        public Object(Vector2 pos, float rotation, Vector2 scale, DrawLayer layer, Scene thescene, List<Component> components)
        {
            Pos = pos;
            Rot = rotation;
            Scale = scale;
            this.layer = layer;
            Components = components;
            foreach (var c in Components)
            {
                c._added(this); //all of these components are added so we should tell them that
            }

            switch (layer)
            {
                case DrawLayer.Bottom:
                    thescene.OnDrawEarly += preDrawUpdate;
                    break;
                case DrawLayer.Middle:
                    thescene.OnDraw += preDrawUpdate;
                    break;
                case DrawLayer.Top:
                    thescene.OnDrawLate += preDrawUpdate;
                    break;
                case DrawLayer.UI:
                    thescene.OnDrawUI += preDrawUpdate;
                    break;
            }
        }

        public void AddComponent(Component component)
        {
            component._added(this);
            Components.Add(component);
        }

        public Component? GetComponent<T>()
        {
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { return item; }
            }
            return null;
        }

        public bool TryGetComponent<T>(out Component? component)
        {
            component = null;
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { component = item; }
            }
            return component!=null;
        }

        public bool TryGetComponent<T>(out Component? component, int index)
        {
            int currindex = 0;
            component = null;
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { if (currindex == index) { component = item; } currindex++; }
            }
            return component != null;
        }

        public Component? GetComponent<T>(int index)
        {
            int currindex = 0;
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { if (currindex == index) { return item; } currindex++; }
            }
            return null;
        }

        public List<Component> GetComponents<T>()
        {
            List<Component> list = new List<Component>();
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { list.Add(item); }
            }
            return list;
        }

        public bool TryGetComponents<T>(out List<Component> components)
        {
            List<Component> list = new();
            foreach (var item in Components)
            {
                if (item.GetType() == typeof(T)) { list.Add(item); }
            }
            components = list;
            return list.Count > 0;
        }

        private void preDrawUpdate(Scene scene)
        {
            foreach (var c in Components)
            {
                c.DrawUpdate();
            }
            OnDrawUpdate(scene);
        }

        protected virtual void OnDrawUpdate(Scene scene)
		{

		}

        /// <summary>
        /// Call this when the object is being destroyed
        /// </summary>
        public void Destroy(Scene scene)
        {
            switch (layer)
            {
                case DrawLayer.Bottom:
                    scene.OnDrawEarly -= preDrawUpdate;
                    break;
                case DrawLayer.Middle:
                    scene.OnDraw -= preDrawUpdate;
                    break;
                case DrawLayer.Top:
                    scene.OnDrawLate -= preDrawUpdate;
                    break;
                case DrawLayer.UI:
                    scene.OnDrawUI -= preDrawUpdate;
                    break;
            }
            foreach (var c in Components)
            {
                c.Destroy();
            }
            OnDestroyed();
        }

        /// <summary>
        /// Remember to clear
        /// </summary>
        protected virtual void OnDestroyed()
        {

        }
    }
}

