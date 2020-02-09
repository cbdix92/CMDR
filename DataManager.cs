using System;
using System.Collections.Generic;

namespace CMDR
{
	public static class DataMGR
	{
		public static GameObjectCollection GameObjects = new GameObjectCollection();
		
		public static Dictionary<ComponentType, List<IComponent>> = new Dictionary<ComponentType, List<IComponent>>();
		
		public static uint GenerateGameObject(IComponent[] components)
		{
			for(uint i = 0; i < GameObjects.Count; i++)
			{
				if(GameObjects[i] == null)
				{
					GameObjects[i] = new GameObject(components);
					return i;
				}
			}
		}
	}
	
	public struct GameObject
	{
		
		public Dictionary<ComponentType, uint> Components;
		
		public GameObject()
		{
			Components = new Dictionary<ComponentType, uint>();
		}
		public GameObject(IComponent[] components)
		{
			Components = new Dictionary<ComponentType, uint>();
			
			Use(components);
		}
		public void Use(IComponent component)
		{
			switch(component.ID)
			{
				case ComponentType.Transform:
					break;
					
				case ComponentType.RenderData:
					break;
					
				case ComponentType.Physics:
					break;
					
			}
		}
		public void Use(IComponent[] components)
		{
			foreach(IComponent component in components)
				Use(component);Components = new Dictionary<ComponentType, uint>();
		}
	}
	public interface IComponent
	{
		public uint parent;
		public ComponentType ID;
	}
	public struct Transform : IComponent
	{
		// IComponent
		public uint parent;
		public ComponentType ID;
		
		private float _x;
		private float _y;
		private int _z;
		

		
		#region Position_Properties
        public float X
        {
            get => _x;
            set
            {
                _x = PositionLogic(value, _x);
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                _y = PositionLogic(value, _y);
            }
        }
        public int Z
        {
            get => _z;
            set
            {
                _z = Math.Min(Math.Abs(value), Render.ZDepth);
            }
        }
        #endregion
		
		#region Velocity_Properties
        public float Xvel
        {
            get => _xvel;
            set
            {
                if (_xvel != value)
                {
					_xvel = value;
                    _parent.Active = true;
                }
            }
        }
        public float Yvel
        {
            get => _yvel;
            set
            {
                if (_yvel != value)
                {
					_yvel = value;
                    _parent.Active = true;
                }
            }
        }
        #endregion
		
		public Transform(uint _parent)
		{
			parent = _parent;
			ID = ComponentType.Transform;
		}
	}
	public struct RenderData : IComponent
	{
		// IComponent
		public uint parent;
		public ComponentType ID;
		
		public RenderData(uint _parent)
		{
			parent = _parent;
			ID = ComponentType.RenderData;
		}
	}
	public struct Collider
	{
		bool[,] ColData;
		
		public Collider(uint _parent)
		{
			parent = _parent;
			ID = ComponentType.Collider;
			
			ColData = BitCollider.GenerateCollisionData(parent);
		}
	}
	public struct Static
	{
		public Static(uint parent)
		{
			parent = _parent;
			ID = ComponentType.Static;
		}
	}
}