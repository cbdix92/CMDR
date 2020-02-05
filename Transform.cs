using System;
using System.Drawing;
using System.Collections.Generic;

namespace CMDR.Components
{
    public class Transform : Component, IComponent
    {
        private float _x;
        private float _y;
        private int _z;
        
        
        private float _xvel;
        private float _yvel;

        private GameObject _parent;

        public List<GameObject> Children;

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
                if (_xvel == 0 && value != 0)
                {
                    _parent.Active = true;
                }
                _xvel = value;
                Children.ForEach(child => child.Transform.Xvel = value);
            }
        }
        public float Yvel
        {
            get => _yvel;
            set
            {
                if (_yvel == 0 && value != 0)
                {
                    _parent.Active = true;
                }
                _yvel = value;
                Children.ForEach(child => child.Transform.Yvel = value);
            }
        }
        #endregion
        internal Transform(GameObject parent, float x = 0, float y = 0, int z = 0) : base (ComponentType.Transform)
        {
            _parent = parent;
            Children = base.Parents;
            X = x;
            Y = y;
            Z = z;
            Xvel = 0;
            Yvel = 0;
        }
        private float PositionLogic(float val, float staticDefault)
        {
            if (_parent.Static)
                return staticDefault;

            if (val != 0 && _parent.Collider)
                SpatialIndexer.CalcGridPos(_parent);

            return val;
        }
        public void Add(GameObject parent)
        {
            Children.Add(parent);
        }
        public void Remove(GameObject parent)
        {
            Children.Remove(parent);
        }
    }
}
