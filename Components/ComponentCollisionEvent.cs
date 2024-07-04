using OpenGL_Game.OBJLoader;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    abstract class ComponentCollisionEvent : IComponent
    {
        public ComponentCollisionEvent()
        {

        }
        public abstract void CollisionEvent(string name);
        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_EVENT; }
        }
    }
}
