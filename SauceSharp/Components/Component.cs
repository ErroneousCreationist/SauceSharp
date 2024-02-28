using System;
namespace SauceSharp.Components
{
	public abstract class Component
	{
		public Object? Owner { protected set; get; }

		public Component()
		{

		}

		public virtual void DrawUpdate() { }

		public virtual void Destroy() { }

		/// <summary>
		/// dont use this
		/// </summary>
		public void _added(Object owner) { Owner = owner; Added(); }

        protected virtual void Added() {  }
	}
}

