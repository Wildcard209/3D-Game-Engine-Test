using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;            
using OpenTK.Audio.OpenAL;
using System.Windows.Forms;

namespace OpenGL_Game.Scenes
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    class GameScene : Scene
    {
        public static float deltaTime = 0;
        public static float time = 0;
        EntityManager entityManager;
        SystemManager systemManager;

        public Camera camera;

        public static GameScene gameInstance;

        List<Vector3> tourchPos = new List<Vector3>();
        List<float> tourchRotation = new List<float>();

        bool[] keysPressed = new bool[255];

        public int lives = 3;
        public int score = 0;
        public int pelletCount = 12;
        public float timePowerup = 0;

        public bool debugWall = false;
        public bool debugGhost = false;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();

            // Set the title of the window
            sceneManager.Title = "Game";
            // Set the Render and Update delegates to the Update and Render methods of this class
            sceneManager.renderer = Render;
            sceneManager.updater = Update;
            // Set Keyboard events to go to a method in this class
            sceneManager.keyboardDownDelegate += Keyboard_KeyDown;
            sceneManager.keyboardUpDelegate += Keyboard_KeyUp;

            // Enable Depth Testing
            //GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            //GL.Enable(EnableCap.CullFace);
           // GL.CullFace(CullFaceMode.Back);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            // Set Camera
            camera = new Camera(new Vector3(0, 0, 20), new Vector3(0, 0, 0), (float)(sceneManager.Width) / (float)(sceneManager.Height), 0.1f, 100f);

            tourchPos.Add(new Vector3(-3.5f, -1.3f, -4.08f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(7.0f, -1.3f, -4.08f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(-3.5f, -1.3f, -19.08f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(7.0f, -1.3f, -19.08f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(-3.5f, -1.3f, 20.99f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(7.0f, -1.3f, 20.99f));
            tourchRotation.Add(0.0f);
            tourchPos.Add(new Vector3(1.8f, -1.3f, -6.06f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(-8.5f, -1.3f, -6.06f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(1.8f, -1.3f, -21.06f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(-8.5f, -1.3f, -21.06f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(1.8f, -1.3f, 18.95f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(-8.5f, -1.3f, 18.95f));
            tourchRotation.Add(1.5708f);
            tourchPos.Add(new Vector3(-6.7f, -1.3f, -11.08f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(3.8f, -1.3f, -11.08f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(-6.7f, -1.3f, -26.08f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(3.8f, -1.3f, -26.08f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(-6.7f, -1.3f, 13.99f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(3.8f, -1.3f, 13.99f));
            tourchRotation.Add(3.14159f);
            tourchPos.Add(new Vector3(8.8f, -1.3f, -9.06f));
            tourchRotation.Add(4.71239f);
            tourchPos.Add(new Vector3(-1.5f, -1.3f, -9.06f));
            tourchRotation.Add(4.71239f);
            tourchPos.Add(new Vector3(8.8f, -1.3f, -24.06f));
            tourchRotation.Add(4.71239f);
            tourchPos.Add(new Vector3(-1.5f, -1.3f, -24.06f));
            tourchRotation.Add(4.71239f);
            tourchPos.Add(new Vector3(8.8f, -1.3f, 15.95f));
            tourchRotation.Add(4.71239f);
            tourchPos.Add(new Vector3(-1.5f, -1.3f, 15.95f));
            tourchRotation.Add(4.71239f);

            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here


        }

        private void CreateEntities()
        {
            Entity newEntity;

            newEntity = new Entity("Wall");
            newEntity.AddComponent(new ComponentPosition(30.0f, -1.5f, -25.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Wall/maze.obj", "Geometry/Wall/Stone_wall_005_COLOR.jpg", "Geometry/Wall/Stone_wall_005_NORM.jpg"));
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDepth(tourchPos));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost1");
            newEntity.AddComponent(new ComponentPosition(-4.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Ghost/ghoast.obj"));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentCollisionEventEnemy());
            newEntity.AddComponent(new ComponentShaderGhost(new Vector4(0.0f, 0.3556f, 0.1036f, 1.0f)));
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new CompoentAIBasic(1.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost2");
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Ghost/ghoast.obj"));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentCollisionEventEnemy());
            newEntity.AddComponent(new ComponentShaderGhost(new Vector4(0.3556f, 0.0f, 0.1036f, 1.0f)));
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new CompoentAIBasic(1.5f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Ghost3");
            newEntity.AddComponent(new ComponentPosition(4.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Ghost/ghoast.obj"));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentCollisionEventEnemy());
            newEntity.AddComponent(new ComponentShaderGhost(new Vector4(0.1036f, 0.3556f, 0.3556f, 1.0f)));
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new CompoentAIBasic(2.0f));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("AudioDeath");
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentAudio("Audio/death.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("AudioKill");
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentAudio("Audio/kill.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("AudioPowerUp");
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentAudio("Audio/powerup.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerUp1");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav", true));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.Play));
            newEntity.AddComponent(new ComponentCollisionBall(5f));
            newEntity.AddComponent(new ComponentCollisionEventPowerUp());
            newEntity.AddComponent(new ComponentPosition(25.0f, -1.0f, 25.0f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerUp2");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav", true));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.Play));
            newEntity.AddComponent(new ComponentCollisionBall(5f));
            newEntity.AddComponent(new ComponentCollisionEventPowerUp());
            newEntity.AddComponent(new ComponentPosition(-20.0f, -1.0f, 25.0f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerUp3");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav", true));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.Play));
            newEntity.AddComponent(new ComponentCollisionBall(5f));
            newEntity.AddComponent(new ComponentCollisionEventPowerUp());
            newEntity.AddComponent(new ComponentPosition(25.0f, -1.0f, -20.0f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("PowerUp4");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Moon/moon.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/buzz.wav", true));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.Play));
            newEntity.AddComponent(new ComponentCollisionBall(5f));
            newEntity.AddComponent(new ComponentCollisionEventPowerUp());
            newEntity.AddComponent(new ComponentPosition(-20.0f, -1.0f, -20.0f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet1");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(8.6f, -1.0f, 23.4f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet2");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(-6.5f, -1.0f, 23.4f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet3");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(-18.2f, -1.0f, 11.8f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet4");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(-18.2f, -1.0f, 4.6f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet5");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(-7.7f, -1.0f, -16.1f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet6");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(8.7f, -1.0f, -16.1f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet7");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(21.1f, -1.0f, -4.6f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Pellet8");
            newEntity.AddComponent(new ComponentGeometry("Geometry/Pellet/moon_key.obj"));
            newEntity.AddComponent(new ComponentAudio("Audio/pellet.wav", false));
            newEntity.AddComponent(new ComponentAudioState(ComponentAudioState.AudioStateEnum.None));
            newEntity.AddComponent(new ComponentCollisionBall(3f));
            newEntity.AddComponent(new ComponentPosition(21.1f, -1.0f, 10.6f));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderDefault());
            newEntity.AddComponent(new ComponentVisable(true));
            newEntity.AddComponent(new ComponentCollisionEventPellet());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Skybox");
            newEntity.AddComponent(new ComponentPosition(-2.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/Skybox/skybox.obj", "Geometry/Skybox/Cubemap.png"));
            newEntity.AddComponent(new ComponentRotation(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentShaderSkybox());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider1");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(10.2f, 0.0f, -8.5f), new Vector3(18.8f, 0.0f, 0.7f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider2");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(4.5f, 0.0f, -13.5f), new Vector3(10.2f, 0.0f, -8.5f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider3");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-15.6f, 0.0f, -7.8f), new Vector3(-6.1f, 0.0f, 1.3f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);


            newEntity = new Entity("Collider4"); 
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-10.5f, 0.0f, -13.3f), new Vector3(-1.8f, 0.0f, -5.0f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider5");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-10f, 0.0f, 11f), new Vector3(-1.3f, 0.0f, 20f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider6");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-16.3f, 0.0f, 6.5f), new Vector3(-10f, 0.0f, 11f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall()); 
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider7");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(4.3f, 0.0f, 11.5f), new Vector3(13.5f, 0.0f, 20.5f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider8");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(9.5f, 0.0f, 7.2f), new Vector3(18.3f, 0.0f, 15.7f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider9");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-10.2f, 0.0f, -24.5f), new Vector3(14.3f, 0.0f, -19.3f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider10");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-28f, 0.0f, -26.5f), new Vector3(30.2f, 0.0f, -24.0f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider11");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-15.6f, 0.0f, -7.8f), new Vector3(-6.1f, 0.0f, 1.3f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);


            newEntity = new Entity("Collider12");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(24.7f, 0.0f, -8.3f), new Vector3(30f, 0.0f, 15.4f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider13");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(29f, 0.0f, -27f), new Vector3(29.2f, 0.0f, 31.2f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider14");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-10.0f, 0.0f, 25.5f), new Vector3(13f, 0.0f, 31.7f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider15");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-26f, 0.0f, 31.0f), new Vector3(35.5f, 0.0f, 37.5f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider16");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-26.6f, 0.0f, -8.5f), new Vector3(-21.5f, 0.0f, 15.5f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Collider17");
            newEntity.AddComponent(new ComponentCollisionBox(new Vector3(-28.5f, 0.0f, -29.2f), new Vector3(-25.9f, 0.0f, 31.1f)));
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentCollisionEventWall());
            newEntity.AddComponent(new ComponentVisable(true));
            entityManager.AddEntity(newEntity);


            for (int i = 0; i < tourchPos.Count; i++)
            {
                newEntity = new Entity($"Tourch{i}");
                newEntity.AddComponent(new ComponentPosition(tourchPos[i]));
                newEntity.AddComponent(new ComponentGeometry("Geometry/Tourch/tourch.obj", "Geometry/Tourch/Stylized_Wood_Planks_001_basecolor.jpg", "Geometry/Tourch/Stylized_Wood_Planks_001_normal.jpg"));
                newEntity.AddComponent(new ComponentRotation(0.0f, tourchRotation[i], 0.0f));
                newEntity.AddComponent(new ComponentShaderDepth(tourchPos));
                newEntity.AddComponent(new ComponentVisable(true));
                entityManager.AddEntity(newEntity);
            }
        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemAudio();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemCollision();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemAI();
            systemManager.AddSystem(newSystem);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Update(FrameEventArgs e)
        {
            deltaTime = (float)e.Time;
            time += deltaTime;
            //System.Console.WriteLine("fps=" + (int)(1.0/dt));

            if (GamePad.GetState(1).Buttons.Back == OpenTK.Input.ButtonState.Pressed)
                sceneManager.Exit();


            if(lives == 0)
            {
                sceneManager.ChangeScene(SceneTypes.SCENE_GAME_OVER);
            }
            // TODO: Add your update logic here

            AL.Listener(ALListener3f.Position, ref camera.cameraPosition);
            AL.Listener(ALListenerfv.Orientation, ref camera.cameraDirection, ref camera.cameraUp);

            Dictionary<char, Action> keyActions = new Dictionary<char, Action>
            {
                { (char)Key.Up, () => camera.MoveForward(0.1f) },
                { (char)Key.W, () => camera.MoveForward(0.1f)},
                { (char)Key.Down, () => camera.MoveForward(-0.1f) },
                { (char)Key.S, () => camera.MoveForward(-0.1f) },
                { (char)Key.Left, () => camera.RotateY(-0.01f) },
                { (char)Key.A, () => camera.RotateY(-0.01f) },
                { (char)Key.Right, () => camera.RotateY(0.01f) },
                { (char)Key.D, () => camera.RotateY(0.01f) },
                { (char)Key.M, () => FlipDebugGhost() },
                { (char)Key.C, () => FlipDebugWall() }
            };

            foreach (var keyValuePair in keyActions)
            {
                if (keysPressed[keyValuePair.Key])
                {
                    keyValuePair.Value.Invoke();
                }
            }


            if(timePowerup > 0)
            {
                timePowerup -= deltaTime;
            }

            if(pelletCount == 0)
            {
                sceneManager.ChangeScene(SceneTypes.SCENE_GAME_WIN);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        public override void Render(FrameEventArgs e)
        {
            GL.Viewport(0, 0, sceneManager.Width, sceneManager.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Action ALL systems
            systemManager.ActionSystems(entityManager);

            // Render score
            float width = sceneManager.Width, height = sceneManager.Height, fontSize = Math.Min(width, height) / 10f;
            GUI.clearColour = Color.Transparent;
            GUI.Label(new Rectangle(0, 0, (int)width, (int)(fontSize * 2f)), $"Lives - {lives}  Score - {score} Power Up Time - {timePowerup}", 22, StringAlignment.Near, Color.White);
            GUI.Render();
        }

        /// <summary>
        /// This is called when the game exits.
        /// </summary>
        public override void Close()
        {
            ResourceManager.RemoveAllAssets();
        }

        public void Keyboard_KeyDown(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = true;
        }

        public void Keyboard_KeyUp(KeyboardKeyEventArgs e)
        {
            keysPressed[(char)e.Key] = false;
        }

        public void FlipDebugWall()
        {
            if (debugWall) 
            {
                debugWall = false;
            } else 
            {
                debugWall = true;
            }
        }

        public void FlipDebugGhost()
        {
            if (debugGhost)
            {
                debugGhost = false;
            }
            else
            {
                debugGhost = true;
            }
        }

        public void EatGhost(string name)
        {
            score += 15;
            Entity ghost = entityManager.FindEntity(name);
            IComponent positionComponent = ghost.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            });

            ((ComponentPosition)positionComponent).Position = new Vector3(0, 0, 0);

            Entity audio = entityManager.FindEntity("AudioKill");

            IComponent audioComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
            });

            ((ComponentAudioState)audioComponent).AudioState = ComponentAudioState.AudioStateEnum.Play;

            positionComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            });

            ((ComponentPosition)positionComponent).Position = camera.cameraPosition;
        }

        public void Death()
        {
            score += 15;
            Entity audio = entityManager.FindEntity("AudioDeath");

            IComponent audioComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
            });

            ((ComponentAudioState)audioComponent).AudioState = ComponentAudioState.AudioStateEnum.Play;

            IComponent positionComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            });

            ((ComponentPosition)positionComponent).Position = camera.cameraPosition;
        }

        public void EatPellet(string name)
        {
            Entity pellet = entityManager.FindEntity(name);

            IComponent visableComponent = pellet.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_VISABLE;
            });

            ((ComponentVisable)visableComponent).Visable = false;

            IComponent audioComponent = pellet.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
            });

            ((ComponentAudioState)audioComponent).AudioState = ComponentAudioState.AudioStateEnum.Play;

            IComponent positionComponent = pellet.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            });

            ((ComponentPosition)positionComponent).Position = camera.cameraPosition;

            score += 5;
            pelletCount -= 1;
        }

        public void EatPowerUp(string name)
        {
            Entity pellet = entityManager.FindEntity(name);

            IComponent visableComponent = pellet.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_VISABLE;
            });

            ((ComponentVisable)visableComponent).Visable = false;

            IComponent audioComponent = pellet.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
            });

            ((ComponentAudioState)audioComponent).AudioState = ComponentAudioState.AudioStateEnum.Pause;

            Entity audio = entityManager.FindEntity("AudioPowerUp");
            audioComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
            });

            ((ComponentAudioState)audioComponent).AudioState = ComponentAudioState.AudioStateEnum.Play;

            IComponent positionComponent = audio.Components.Find(delegate (IComponent component)
            {
                return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
            });

            ((ComponentPosition)positionComponent).Position = camera.cameraPosition;

            score += 10;
            timePowerup += 15;
            pelletCount -= 1;
        }
    }
}
