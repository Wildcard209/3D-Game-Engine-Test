using OpenGL_Game.Managers;
using OpenGL_Game.OBJLoader;

namespace OpenGL_Game.Components
{
    class ComponentGeometry : IComponent
    {
        Geometry geometry;

        public ComponentGeometry(string geometryName)
        {
            this.geometry = ResourceManager.LoadGeometry(geometryName);
        }

        public ComponentGeometry(string geometryName, string baseName)
        {
            this.geometry = ResourceManager.LoadGeometry(geometryName, baseName);
        }

        public ComponentGeometry(string geometryName, string baseName, string normalName)
        {
            this.geometry = ResourceManager.LoadGeometry(geometryName, baseName, normalName);
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_GEOMETRY; }
        }

        public Geometry Geometry()
        {
            return geometry;
        }
    }
}
