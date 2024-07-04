using OpenGL_Game.Scenes;
using OpenTK;
using System;

namespace OpenGL_Game.Components
{
    class ComponentCollisionBox : ComponentCollision
    {
        Vector3 lowerBound;
        Vector3 upperBound;

        public ComponentCollisionBox(float lowerX, float lowerY, float lowerZ, float higherX, float higherY, float higherZ) : base()
        {
            lowerBound = new Vector3(lowerX, lowerY, lowerZ);
            upperBound = new Vector3(higherX, higherY, higherZ);
        }

        public ComponentCollisionBox(Vector3 lower, Vector3 higher) : base()
        {
            lowerBound = lower;
            upperBound = higher;
        }

        public Vector3 LowerBound
        {
            get { return lowerBound; }
        }

        public Vector3 UpperBound
        {
            get { return upperBound; }
        }

        public override bool IsColliding(Vector3 point)
        {
            if (GameScene.gameInstance.camera.cameraPosition.X >= lowerBound.X && GameScene.gameInstance.camera.cameraPosition.X <= upperBound.X &&
                GameScene.gameInstance.camera.cameraPosition.Y >= lowerBound.Y && GameScene.gameInstance.camera.cameraPosition.Y <= upperBound.Y &&
                GameScene.gameInstance.camera.cameraPosition.Z >= lowerBound.Z && GameScene.gameInstance.camera.cameraPosition.Z <= upperBound.Z)
            {
                return true;
            }
            return false;
        }
    }
}
