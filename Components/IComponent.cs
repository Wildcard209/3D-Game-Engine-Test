﻿using System;

namespace OpenGL_Game.Components
{
    [FlagsAttribute]
    enum ComponentTypes {
        COMPONENT_NONE     = 0,
	    COMPONENT_POSITION = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_TEXTURE  = 1 << 2,
        COMPONENT_SHADER = 1 << 3,
        COMPONENT_VELOCITY = 1 << 4,
        COMPONENT_ROTATION = 1 << 5,
        COMPONENT_AUDIO = 1 << 6,
        COMPONENT_AUDIO_STATE = 1 << 7,
        COMPONENT_COLLISION = 1 << 8,
        COMPONENT_COLLISION_EVENT = 1 << 9,
        COMPONENT_AI = 1 << 10,
        COMPONENT_VISABLE = 1 << 11
    }

    interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}
