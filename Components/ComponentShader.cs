using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL_Game.Managers;
using System.Drawing.Drawing2D;
using OpenGL_Game.OBJLoader;
using OpenTK;

namespace OpenGL_Game.Components
{
    abstract class ComponentShader : IComponent
    {
        public int programID;

        public ComponentShader(string vertexShader, string fragmentShader)
        {
            programID = GL.CreateProgram();
            GL.AttachShader(programID, ResourceManager.LoadShader(vertexShader, ShaderType.VertexShader));
            GL.AttachShader(programID, ResourceManager.LoadShader(fragmentShader, ShaderType.FragmentShader));
            GL.LinkProgram(programID);
        }

        public abstract void ApplyShader(Matrix4 model, Geometry geometry);

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_SHADER; }
        }
    }
}
