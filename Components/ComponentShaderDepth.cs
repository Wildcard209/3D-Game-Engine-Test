using OpenGL_Game.OBJLoader;
using OpenGL_Game.Scenes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentShaderDepth : ComponentShader
    {
        public int uniform_texture;
        public int uniform_texture_normal;
        public int uniform_texture_hight;
        public int uniform_model;
        public int uniform_modelViewProjection;
        public int uniform_modelView;
        public int uniform_diffuse;
        public int uniform_viewPosition;
        public int uniform_lights;

        List<Vector3> lights;

        public ComponentShaderDepth(List<Vector3> inLights) : base("Shaders/vs-depth.glsl", "Shaders/fs-depth.glsl")
        {
            lights = inLights;

            uniform_texture = GL.GetUniformLocation(programID, "baseMap");
            uniform_texture_normal = GL.GetUniformLocation(programID, "normalMap");
            uniform_texture_hight = GL.GetUniformLocation(programID, "heightMap");
            uniform_modelViewProjection = GL.GetUniformLocation(programID, "ModelViewProjMat");
            uniform_modelView = GL.GetUniformLocation(programID, "ModelViewMat");
            uniform_model = GL.GetUniformLocation(programID, "ModelMat");
            uniform_viewPosition = GL.GetUniformLocation(programID, "ViewPosition");
            uniform_diffuse = GL.GetUniformLocation(programID, "v_diffuse");
            //uniform_lights = GL.GetUniformLocation(programID, "LightPosistions[24]");
        }

        public override void ApplyShader(Matrix4 model, Geometry geometry)
        {
            GL.UseProgram(programID);
            
            for(int i = 0; i < 24; i++)
            {
                uniform_lights = GL.GetUniformLocation(programID, $"LightPosistions[{i}]");
                GL.Uniform3(uniform_lights, lights[i]);
            }

            GL.Uniform1(uniform_texture, 0);
            GL.Uniform1(uniform_texture_normal, 1);

            GL.UniformMatrix4(uniform_model, false, ref model);
            
            Matrix4 modelViewProjection = model * GameScene.gameInstance.camera.view * GameScene.gameInstance.camera.projection;
            GL.UniformMatrix4(uniform_modelViewProjection, false, ref modelViewProjection);

            Matrix4 view = GameScene.gameInstance.camera.view;
            Matrix4 modelNormal = modelViewProjection * Matrix4.Invert(view);
            Matrix4 modelView = modelNormal * view;

            GL.UniformMatrix4(uniform_modelView, false, ref modelView);

            geometry.Render(uniform_diffuse);

            GL.UseProgram(0);
        }
    }
}
