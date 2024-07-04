using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentRotation : IComponent
    {
        Vector3 rotation;

        public ComponentRotation(float x, float y, float z)
        {
            rotation = new Vector3(x, y, z);
        }

        public ComponentRotation(Vector3 rotationVector)
        {
            rotation = rotationVector;
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_ROTATION; }
        }
    }
}
