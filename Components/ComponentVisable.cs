using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentVisable : IComponent
    {
        bool visable;

        public ComponentVisable(bool isVisable)
        {
            visable = isVisable;
        }

        public bool Visable
        {
            get { return visable; }
            set { visable = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_VISABLE; }
        }
    }
}
