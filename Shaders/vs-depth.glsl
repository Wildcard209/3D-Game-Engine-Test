#version 330

layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec2 a_TexCoord;
layout (location = 2) in vec3 a_Normal;

uniform mat4 ModelViewProjMat;
uniform mat4 ModelViewMat;
uniform mat4 ModelMat;

out vec2 TexCoord;
out vec3 FragPos;
out vec3 ViewDirection;
out vec3 LightDirection;

void main()
{
	gl_Position = ModelViewProjMat * vec4(a_Position, 1.0);
	FragPos = vec3(ModelMat * vec4(a_Position, 1.0));
	TexCoord = a_TexCoord;

	vec4 objectPosition = ModelViewMat * vec4(a_Position, 1.0);
	ViewDirection =  -objectPosition.xyz;
	LightDirection = vec3(0,0,0) - a_Position;
}