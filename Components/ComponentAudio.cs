using OpenGL_Game.Managers;
using OpenTK.Audio.OpenAL;

namespace OpenGL_Game.Components
{
    class ComponentAudio : IComponent
    {
        int audioSource;
        bool isLooping;

        public ComponentAudio(string audioFile, bool looping)
        {
            audioSource = AL.GenSource();
            isLooping = looping;
            int audioBuffer = ResourceManager.LoadAudio(audioFile);
            AL.Source(audioSource, ALSourcei.Buffer, audioBuffer);
            AL.Source(audioSource, ALSourceb.Looping, isLooping);
            
        }

        public int Audio
        {
            get { return audioSource; }
        }

        public bool IsLooping
        { 
            get { return isLooping; } 
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIO; }
        }
    }
}
