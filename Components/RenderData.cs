using System;
using System.IO;
using System.Collections.Generic;

namespace CMDR
{
    public class RenderData : Component
    {
        // Holds all the possible States and associated RenderData
        public List<RenderState> Data;

        // Holds unique GameObjects and their associated RenderState
        public Dictionary<GameObject, RenderState> GameObjectStates { get; internal set; }

        public RenderData() : base (ComponentType.RenderData)
        {
            Data = new List<RenderState>();
            GameObjectStates = new Dictionary<GameObject, RenderState>();
        }
        internal override void NewParent(GameObject parent)
        {
            if(!GameObjectStates.ContainsKey(parent) && Data != null)
            {
                GameObjectStates.Add(parent, Data[0]);
                parent.Parent.RenderObjects.Add(parent);
            }
        }
        public void RemoveParent(GameObject parent)
        {
            if(GameObjectStates.ContainsKey(parent))
            {
                GameObjectStates.Remove(parent);
                parent.Parent.RenderObjects.Remove(parent);
            }
        }
        public override RenderState LoadFile(string src)
        {
            try
            {
                return LoadImage(System.Drawing.Image.FromFile(src));
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"The file: '{src}' Could not be found!");
            }
        }
        public override RenderState LoadImage(System.Drawing.Image image)
        {
            Data.Add(new RenderState(this, image));
            return Data[Data.Count - 1];
        }
        public override System.Drawing.Image GetRenderData(GameObject parent)
        {
            return GameObjectStates[parent].GetData();
        }
    }
    public class RenderState
    {
        private RenderData _parent;
        private System.Drawing.Image _data;
        public GameObject State
        {
            set
            {
                _parent.GameObjectStates[value] = this;
            }
        }

        public RenderState(RenderData parent, System.Drawing.Image data)
        {
            _parent = parent;
            _data = data;
        }
        public System.Drawing.Image GetData()
        {
            return _data;
        }
    }
}
