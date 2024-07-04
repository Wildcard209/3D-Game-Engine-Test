using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.OBJLoader;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;

namespace OpenGL_Game.Systems
{
    class SystemRender : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_GEOMETRY | ComponentTypes.COMPONENT_SHADER | ComponentTypes.COMPONENT_ROTATION | ComponentTypes.COMPONENT_VISABLE);

        //protected int pgmID;
        //protected int vsID;
        //protected int fsID;
        //protected int uniform_stex;
        //protected int uniform_mmodelviewproj;
        //protected int uniform_mmodel;
        //protected int uniform_diffuse;  // OBJ NEW

        public SystemRender()
        {
            //pgmID = GL.CreateProgram();
            //LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            //LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            //GL.LinkProgram(pgmID);
            //Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            //uniform_stex = GL.GetUniformLocation(pgmID, "s_texture");
            //uniform_mmodelviewproj = GL.GetUniformLocation(pgmID, "ModelViewProjMat");
            //uniform_mmodel = GL.GetUniformLocation(pgmID, "ModelMat");
            //uniform_diffuse = GL.GetUniformLocation(pgmID, "v_diffuse");     // OBJ NEW
        }

        //void LoadShader(String filename, ShaderType type, int program, out int address)
        //{
        //    address = GL.CreateShader(type);
        //    using (StreamReader sr = new StreamReader(filename))
        //    {
        //        GL.ShaderSource(address, sr.ReadToEnd());
        //    }
        //    GL.CompileShader(address);
        //    GL.AttachShader(program, address);
        //    Console.WriteLine(GL.GetShaderInfoLog(address));
        //}

        public string Name
        {
            get { return "SystemRender"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent geometryComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_GEOMETRY;
                });
                Geometry geometry;

                geometry = ((ComponentGeometry)geometryComponent).Geometry();

                IComponent positionComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                Vector3 position = ((ComponentPosition)positionComponent).Position;

                IComponent rotationComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_ROTATION;
                });
                Vector3 rotation = ((ComponentRotation)rotationComponent).Rotation;

                Matrix4 translationMatrix = Matrix4.CreateTranslation(position);
                Matrix4 rotationXMatrix = Matrix4.CreateRotationX(rotation.X);
                Matrix4 rotationYMatrix = Matrix4.CreateRotationY(rotation.Y);
                Matrix4 rotationZMatrix = Matrix4.CreateRotationZ(rotation.Z);

                Matrix4 model = translationMatrix * rotationXMatrix * rotationYMatrix * rotationZMatrix;

                IComponent shaderComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_SHADER;
                });
                ComponentShader shader = ((ComponentShader)shaderComponent);

                IComponent visableComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_VISABLE;
                });

                bool isVisible = ((ComponentVisable)visableComponent).Visable;
                if (isVisible)
                {
                    shader.ApplyShader(model, geometry);
                }
            }
        }

        //public void Draw(Matrix4 model, Geometry geometry, ComponentShader shader)
        //{
            //GL.UseProgram(pgmID);

            //GL.Uniform1(uniform_stex, 0);
            //GL.ActiveTexture(TextureUnit.Texture0);

            //GL.UniformMatrix4(uniform_mmodel, false, ref model);
            //Matrix4 modelViewProjection = model * GameScene.gameInstance.camera.view * GameScene.gameInstance.camera.projection;
            //GL.UniformMatrix4(uniform_mmodelviewproj, false, ref modelViewProjection);

            //geometry.Render(uniform_diffuse);   // OBJ CHANGED

            //GL.UseProgram(0);
        //}
    }
}
