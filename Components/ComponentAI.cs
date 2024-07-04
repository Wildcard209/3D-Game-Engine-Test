using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    abstract class ComponentAI : IComponent
    {
        public ComponentAI() { }

        public abstract void MoveAI(ComponentPosition position);

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AI; }
        }
    }
}
