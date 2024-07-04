#version 330
 
in vec2 v_TexCoord;
in vec3 v_Normal;
in vec3 v_FragPos;

uniform sampler2D s_texture;
uniform vec3 v_diffuse;	// OBJ NEW

uniform vec2 fViewportDimensions;
uniform float fGlowStrenght;
uniform vec4 vGlowColor;

out vec4 Color;
 
void main()
{
	vec4 objectColor = vGlowColor;
    vec2 screenCenter = fViewportDimensions / 2.0;
    float distanceToCenter = length(v_TexCoord * fViewportDimensions - screenCenter);

    float glowRadius = 0.0;
    float glowStrength = 5.0;

    float glowFactor = smoothstep(glowRadius, glowRadius + 0.01, glowRadius - distanceToCenter);

    float transparency = 0.1;

    vec4 finalColor = mix(vec4(0.0), objectColor, glowFactor) * transparency + objectColor * (1.0 - transparency);

    Color = vec4(finalColor.rgb * finalColor.a, finalColor.a);
}