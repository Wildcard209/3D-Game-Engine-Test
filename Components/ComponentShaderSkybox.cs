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
    class ComponentShaderSkybox : ComponentShader
    {
        public int uniform_texture;
        public int uniform_model;
        public int uniform_modelViewProjection;
        public int uniform_diffuse;

        public ComponentShaderSkybox() : base("Shaders/vs-skybox.glsl", "Shaders/fs-skybox.glsl")
        {
            uniform_texture = GL.GetUniformLocation(programID, "s_texture");
            uniform_modelViewProjection = GL.GetUniformLocation(programID, "ModelViewProjMat");
            uniform_model = GL.GetUniformLocation(programID, "ModelMat");
            uniform_diffuse = GL.GetUniformLocation(programID, "v_diffuse");
        }

        public override void ApplyShader(Matrix4 model, Geometry geometry)
        {
            GL.UseProgram(programID);

            GL.Uniform1(uniform_texture, 0);
            GL.ActiveTexture(TextureUnit.Texture0);

            //Overide model location to be where the camra is insted
            model = Matrix4.CreateTranslation(GameScene.gameInstance.camera.cameraPosition);

            GL.UniformMatrix4(uniform_model, false, ref model);
            Matrix4 modelViewProjection = model * GameScene.gameInstance.camera.view * GameScene.gameInstance.camera.projection;

            GL.UniformMatrix4(uniform_modelViewProjection, false, ref modelViewProjection);

            geometry.Render(uniform_diffuse);

            GL.UseProgram(0);
        }
    }
}
