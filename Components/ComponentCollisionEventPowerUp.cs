using OpenGL_Game.Managers;
using OpenGL_Game.Scenes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentCollisionEventPowerUp : ComponentCollisionEvent
    {
        public ComponentCollisionEventPowerUp() : base() { }
        public override void CollisionEvent(string name)
        {
            GameScene.gameInstance.EatPowerUp(name);
        }
    }
}
