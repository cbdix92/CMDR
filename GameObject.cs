using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public class GameObject
    {
        public Scene Parent;
        public Transform Transform;
        public List<List<GameObject>> CurrentCells = new List<List<GameObject>>();
        public Dictionary<string, Componet> Componets = new Dictionary<string, Componet>();
        public int Width
        {
            get
            {
                try
                {
                    return Componets["Image"].GetImage().Width;
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
                    return Componets["Image"].GetImage().Height;
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

        public GameObject(Scene parent, int posX, int posY, int posZ)
        {
            Parent = parent;
            Transform = new Transform(this, posX, posY, posZ);
        }
        public void AddComponet(Componet Componet)
        {
            Componets.Add(Componet.ID, Componet);
        }
        public System.Drawing.Image GetRenderData()
        {
            return Componets["Image"].GetImage();
        }
    }
}
