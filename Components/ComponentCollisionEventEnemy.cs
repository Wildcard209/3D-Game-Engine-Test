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
    class ComponentCollisionEventEnemy : ComponentCollisionEvent
    {
        public ComponentCollisionEventEnemy() : base() { }
        public override void CollisionEvent(string name)
        {
            if (GameScene.gameInstance.timePowerup > 0)
            {
                GameScene.gameInstance.EatGhost(name);
            }
            else
            {
                GameScene.gameInstance.Death();
                GameScene.gameInstance.camera.cameraPosition = new Vector3(0, 0, 7);
                GameScene.gameInstance.lives -= 1;
            }
        }
    }
}
