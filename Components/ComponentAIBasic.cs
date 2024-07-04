using OpenGL_Game.Scenes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class CompoentAIBasic : ComponentAI
    {
        float speed;
        public CompoentAIBasic(float inSpeed) : base()
        {
            speed = inSpeed;
        }
        public override void MoveAI(ComponentPosition position)
        {
            if (GameScene.gameInstance.debugGhost) { }
            else
            {
                Vector3 direction = Vector3.Normalize(GameScene.gameInstance.camera.cameraPosition - position.Position);
                position.Position += direction * speed * GameScene.deltaTime;
            }
        }
    }
}
