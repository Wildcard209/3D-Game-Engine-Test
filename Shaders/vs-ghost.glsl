﻿#version 330

layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec2 a_TexCoord;
layout (location = 2) in vec3 a_Normal;

uniform mat4 ModelViewProjMat;
uniform mat4 ModelMat;
uniform float fTime0_X;

out vec2 v_TexCoord;
out vec3 v_Normal;
out vec3 v_FragPos;

void main()
{
	float moveY = (sin(fTime0_X) * 0.8f);

	vec4 position = vec4(a_Position, 1.0);

	position.y += moveY;

	gl_Position = ModelViewProjMat * position;
	v_FragPos = vec3(ModelMat * position);
	v_TexCoord = a_TexCoord;
	v_Normal = vec3(transpose(inverse(ModelMat)) * vec4(a_Normal, 1.0));
}