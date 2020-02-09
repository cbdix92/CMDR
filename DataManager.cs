using System;
using System.Collections.Generic;

namespace CMDR
{
	public static class DataMGR
	{
		public static List<GameObject> GameObjects = new List<GameObject>();
		
		public static Dictionary<Type, List<IComponent>>  Components = new Dictionary<Type, List<IComponent>>();
		
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
	public class GameObjectCollection : CollectionBase
	{
		public GameObject this[uint index]
		{
			get => this.List[index];
			set
			{
				value.Handle = index;
				this.List[index] = value;
			}
		}
		public GameObjectCollection()
		{
			
		}
	}
	
	public class GameObject
	{
		private uint _handle;
		public Dictionary<Type, uint> Components;
		public uint Handle
		{
			get => _handle;
			set
			{
				if(value != _handle)
				{
					_handle = value;
					foreach(Type t in Components)
						Components[t].Parent = _handle;
				}
			}
		}
		
		public GameObject()
		{
			Components = new Dictionary<Type, uint>();
		}
		public GameObject(IComponent[] components)
		{
			Components = new Dictionary<Type, uint>();
			
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
				Use(component);
		}
	}
	public interface IComponent
	{
		public uint Parent;
		public ComponentType ID;
	}
	public struct Transform : IComponent
	{
		// IComponent
		public uint Parent;
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
		
		public Transform(uint parent)
		{
			Parent = parent;
			ID = ComponentType.Transform;
		}
	}
	public struct RenderData : IComponent
	{
		// IComponent
		public uint Parent;
		public ComponentType ID;
		
		public RenderData(uint parent)
		{
			Parent = parent;
			ID = ComponentType.RenderData;
		}
	}
	public struct Collider
	{
		bool[,] ColData;
		public uint Parent;
		
		public Collider(uint parent)
		{
			Parent = parent;
			ID = ComponentType.Collider;
			
			ColData = BitCollider.GenerateCollisionData(parent);
		}
	}
	public struct Static
	{
		public uint Parent;
		public Static(uint parent)
		{
			Parent = parent;
			ID = ComponentType.Static;
		}
	}
}