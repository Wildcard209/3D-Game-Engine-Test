using OpenGL_Game.Managers;
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
    class ComponentShaderGhost : ComponentShader
    {
        public int uniform_texture;
        public int uniform_model;
        public int uniform_modelViewProjection;
        public int uniform_diffuse;

        public int uniform_viewportDimensions;
        public int uniform_glowStrenght;
        public int uniform_glowColor;
        
        public int uniform_time;

        Vector4 colour;
        public ComponentShaderGhost(Vector4 inColour) : base("Shaders/vs-ghost.glsl", "Shaders/fs-ghost.glsl")
        {
            colour = inColour;

            uniform_texture = GL.GetUniformLocation(programID, "s_texture");
            uniform_modelViewProjection = GL.GetUniformLocation(programID, "ModelViewProjMat");
            uniform_model = GL.GetUniformLocation(programID, "ModelMat");
            uniform_diffuse = GL.GetUniformLocation(programID, "v_diffuse");

            uniform_viewportDimensions = GL.GetUniformLocation(programID, "fViewportDimensions");
            uniform_glowStrenght = GL.GetUniformLocation(programID, "fGlowStrenght");
            uniform_glowColor = GL.GetUniformLocation(programID, "vGlowColor");
            uniform_time = GL.GetUniformLocation(programID, "fTime0_X");
        }

        public override void ApplyShader(Matrix4 model, Geometry geometry)
        {
            GL.UseProgram(programID);

            GL.Uniform1(uniform_texture, 0);
            GL.ActiveTexture(TextureUnit.Texture0);

            GL.UniformMatrix4(uniform_model, false, ref model);
            Matrix4 modelViewProjection = model * GameScene.gameInstance.camera.view * GameScene.gameInstance.camera.projection;
            GL.UniformMatrix4(uniform_modelViewProjection, false, ref modelViewProjection);

            GL.Uniform1(uniform_glowStrenght, 0.58f);
            GL.Uniform2(uniform_viewportDimensions, new Vector2(SceneManager.height, SceneManager.width));
            GL.Uniform4(uniform_glowColor,colour);

            GL.Uniform1(uniform_time, GameScene.time);

            geometry.Render(uniform_diffuse);

            GL.UseProgram(0);
            
        }
    }
}
