using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Components;
using OpenGL_Game.OBJLoader;
using OpenGL_Game.Objects;
using OpenGL_Game.Scenes;
using OpenTK.Audio.OpenAL;
using static OpenGL_Game.Components.ComponentAudioState;

namespace OpenGL_Game.Systems
{
    class SystemAudio : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_AUDIO | ComponentTypes.COMPONENT_AUDIO_STATE);

        public SystemAudio()
        {
        }

        public string Name
        {
            get { return "SystemAudio"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent audioComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AUDIO;
                });
                int audio = ((ComponentAudio)audioComponent).Audio;

                IComponent positionComponent = components.Find(delegate(IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                Vector3 position = ((ComponentPosition)positionComponent).Position;

                IComponent audioStateComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_AUDIO_STATE;
                });
                AudioStateEnum audioState = ((ComponentAudioState)audioStateComponent).AudioState;
                bool isRepeating = ((ComponentAudio)audioComponent).IsLooping;

                switch (audioState)
                {
                    case AudioStateEnum.Play : 
                        PlaySound(position, audio);
                        if(isRepeating)
                        {
                            ((ComponentAudioState)audioStateComponent).AudioState = AudioStateEnum.Playing;
                        }
                        else
                        {
                            ((ComponentAudioState)audioStateComponent).AudioState = AudioStateEnum.None;
                        }

                        break;
                    case AudioStateEnum.Pause:
                        StopSound(audio);
                        break;
                }
            }
        }

        public void PlaySound(Vector3 componentPosition, int componentAudio)
        {
            AL.Source(componentAudio, ALSource3f.Position, ref componentPosition);
            AL.SourcePlay(componentAudio);
        }

        public void StopSound(int componentAudio)
        {
            AL.SourcePause(componentAudio);
        }
    }
}
