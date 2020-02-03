using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR.Components
{
    internal interface IComponent
    {
        void Add(GameObject parent);

        void Remove(GameObject parent);
    }
}
