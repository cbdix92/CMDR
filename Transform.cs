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
		
        public GameObject Parent;

        public float X
        {
            get => _x;
            set
            {
                _x = value;
                if (value != 0 && Parent.Collider && !Parent.Static)
                {
                    SpatialIndexer.CalcGridPos(Parent);
                }
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                if (value != 0 && Parent.Collider && !Parent.Static)
                {
                    SpatialIndexer.CalcGridPos(Parent);
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

        public float Xvel
        {
            get => _xvel;
            set
            {
                if (_xvel == 0 && value != 0)
                {
                    Parent.Active = true;
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
                    Parent.Active = true;
                }
                _yvel = value;
            }
        }
        public Transform(GameObject parent, float x = 0, float y = 0, int z = 0)
        {
            Parent = parent;
            X = x;
            Y = y;
            Z = z;
            Xvel = 0;
            Yvel = 0;
        }
    }
}
