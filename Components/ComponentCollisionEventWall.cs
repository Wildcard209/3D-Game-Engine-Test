using OpenGL_Game.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentCollisionEventWall : ComponentCollisionEvent
    {
        public ComponentCollisionEventWall() : base() { }
        public override void CollisionEvent(string name)
        {
            if (GameScene.gameInstance.debugWall) { }
            else
            {
                GameScene.gameInstance.camera.MoveForward(-0.2f);
            }
        }
    }
}
