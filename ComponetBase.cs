using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CMDR
{
    public abstract class Componet
    {
        public string ID = "";
        public GameObject Parent;
        
        public virtual bool ColliderAtIndex(int x, int y) { return false; }
        public virtual System.Drawing.Image GetImage() { return null; }
    }
}
