using System;
using Raylib_cs;
namespace SauceSharp
{
	public class Scene
	{
        public Action<Scene>? OnDrawEarly, OnDraw, OnDrawLate, OnDrawUI, OnSceneLoaded, OnSceneUnloaded;
        protected Action<Scene>? Initiate;
        public List<Object> Objects;
        public Color bgCol;
        public Game? TheGame;

        public Scene(Color bgcol, Action<Scene> InitiateScene)
		{
            TheGame = null;
            bgCol = bgcol;
            Objects = new List<Object>();
            Initiate = InitiateScene;
		}

        public virtual void Loaded(Game game)
        {
            TheGame = game;
            OnSceneLoaded?.Invoke(this);
            Initiate?.Invoke(this);
        }

        public virtual void Unloaded()
        {
            OnSceneUnloaded?.Invoke(this);
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Destroy(this);
            }
            Objects = new List<Object>();
        }

        public virtual void SpawnObject(Object ob)
        {
            Objects.Add(ob);
        }

        public virtual void DestroyObject(Object ob)
        {
            ob.Destroy(this);
            Objects.Remove(ob);
        }

        public virtual void Draw()
        {
            OnDrawEarly?.Invoke(this); //first (bottom) layer
            OnDraw?.Invoke(this); //second (middle) layer
            OnDrawLate?.Invoke(this); //third (top) layer
            OnDrawUI?.Invoke(this); //reserved for UI, drawn on top of everything
        }
    }
}

