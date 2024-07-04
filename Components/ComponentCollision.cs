using OpenGL_Game.OBJLoader;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    abstract class ComponentCollision : IComponent
    {
        public ComponentCollision()
        {

        }
        public abstract bool IsColliding(Vector3 point);
        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION; }
        }
    }
}
