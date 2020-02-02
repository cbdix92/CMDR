using System;
using System.Drawing;

namespace CMDR
{
    public class Transform
    {
        private float _x;
        private float _y;
        private int _z;
        
        
        private float _xvel;
        private float _yvel;
		
       
        private GameObject _parent;

        #region Position_Properties
        public float X
        {
            get => _x;
            set
            {
                if (!_parent.Static)
                {
                    _x = value;
                }
                if (value != 0 && _parent.Collider && !_parent.Static)
                {
                    SpatialIndexer.CalcGridPos(_parent);
                }
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                if (!_parent.Static)
                {
                    _y = value;
                }
                if (value != 0 && _parent.Collider && !_parent.Static)
                {
                    SpatialIndexer.CalcGridPos(_parent);
                }
            }
        }
        public int Z
        {
            get => _z;
            set
            {
                if (value > Render.ZDepth)
                {
                    _z = Render.ZDepth;
                }
                else _z = Math.Max(0, value);
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
            }
        }
        #endregion
        public Transform(GameObject parent, float x = 0, float y = 0, int z = 0)
        {
            _parent = parent;
            X = x;
            Y = y;
            Z = z;
            Xvel = 0;
            Yvel = 0;
        }
    }
}
