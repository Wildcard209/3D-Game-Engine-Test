using OpenGL_Game.Managers;

namespace OpenGL_Game.Components
{
    class ComponentAudioState : IComponent
    {
        AudioStateEnum audioState;

        public ComponentAudioState(AudioStateEnum state)
        {
            audioState = state;
        }

        public AudioStateEnum AudioState
        {
            get { return audioState; } 
            set { audioState = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIO_STATE; }
        }

        public enum AudioStateEnum
        {
            None = 0,
            Play,
            Playing,
            Pause
        }
    }
}
