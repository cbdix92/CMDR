using System;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObject
    {
        public Scene Parent;
        public Transform Transform;
        public List<List<GameObject>> CurrentCells = new List<List<GameObject>>();
        public Dictionary<ComponentType, Component> Components = new Dictionary<ComponentType, Component>();
        public int Width
        {
            get
            {
                try
                {
                    return Components[ComponentType.Image].GetRenderData().Width;
                }
                catch
                {
                    return 1;
                }
            }
        }
        public int Height
        {
            get
            {
                try
                {
                    return Components[ComponentType.Image].GetRenderData().Height;
                }
                catch
                {
                    return 1;
                }
            }
        }
        private bool _active;
        public bool Active 
        { 
            get => _active; 
            set
            {
                bool Validate = Parent.ActiveGameObjects.Contains(this);
                if (value && !Validate) Parent.ActiveGameObjects.Add(this);
                else if (!value && Validate) Parent.ActiveGameObjects.Remove(this);
                _active = value;
            }
        }
        private bool _collider;
        public bool Collider 
        {
            get => _collider;
            set
            {
                bool Validate = Parent.ColliderGameObjects.Contains(this);
                if (value && !Validate) Parent.ColliderGameObjects.Add(this);
                else if (!value && Validate) Parent.ColliderGameObjects.Remove(this);
                _collider = value;
            }
        }

        public bool PhysicsFlag { get; set; }

        public GameObject(Scene parent, int posX, int posY, int posZ)
        {
            Parent = parent;
            Transform = new Transform(this, posX, posY, posZ);
        }
        public void AddComponet(Component Componet)
        {
            Components.Add(Componet.ID, Componet);
        }
        public System.Drawing.Image GetRenderData()
        {
            return Components[ComponentType.Image].GetRenderData();
        }
        private bool Exist(ComponentType component)
        {
            return Components.ContainsKey(component);
        }
    }
}
