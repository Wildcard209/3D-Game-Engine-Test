using OpenGL_Game.Scenes;
using OpenTK;
using System;

namespace OpenGL_Game.Components
{
    class ComponentCollisionBall : ComponentCollision
    {
        float radius;

        public ComponentCollisionBall(float r) : base()
        {
            radius = r;
        }

        public float Radius
        {
            get { return radius; }
        }

        public override bool IsColliding(Vector3 point)
        {
            float distance = (float)Math.Sqrt(
                (GameScene.gameInstance.camera.cameraPosition.X - point.X) * (GameScene.gameInstance.camera.cameraPosition.X - point.X)
                + (GameScene.gameInstance.camera.cameraPosition.Y - point.Y) * (GameScene.gameInstance.camera.cameraPosition.Y - point.Y)
                + (GameScene.gameInstance.camera.cameraPosition.Z - point.Z) * (GameScene.gameInstance.camera.cameraPosition.Z - point.Z));
            return distance < radius;
        }
    }
}
