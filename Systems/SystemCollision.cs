using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Systems
{
    class SystemCollision : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_COLLISION | ComponentTypes.COMPONENT_COLLISION_EVENT | ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_VISABLE);

        public SystemCollision()
        {
        }

        public string Name
        {
            get { return "SystemCollision"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent collisionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION;
                });
                ComponentCollision collision = ((ComponentCollision)collisionComponent);

                IComponent collisionEventComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_EVENT;
                });
                ComponentCollisionEvent collisionEvent = ((ComponentCollisionEvent)collisionEventComponent);

                IComponent posisionComonent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });

                Vector3 posision = ((ComponentPosition)posisionComonent).Position;

                IComponent visableComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VISABLE;
                });

                bool isVisible = ((ComponentVisable)visableComponent).Visable;
                if (isVisible)
                {
                    if (collision.IsColliding(posision))
                    {
                        collisionEvent.CollisionEvent(entity.Name);
                    }
                }
            }
        }
    }
}
