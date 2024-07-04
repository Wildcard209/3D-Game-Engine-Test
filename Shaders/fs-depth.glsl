#version 330
 
in vec2 TexCoord;
in vec3 FragPos;
in vec3 ViewDirection;
in vec3 LightDirection;

uniform vec3 LightPosistions[24];

uniform sampler2D baseMap;
uniform sampler2D normalMap;

//uniform vec3 v_diffuse;	// OBJ NEW

out vec4 Color;

void main()
{
    vec3 viewDirection = normalize(ViewDirection);
   
    vec2 baseTexCoord = mod(TexCoord * vec2(1.0f, 1.0f), 1.0);
    vec4 baseColor = texture2D(baseMap, TexCoord);
   
    vec3 normal = normalize((texture2D(normalMap, TexCoord).xyz * 2.0) - 1.0);

    vec4 totalAmbient = vec4(0.0);
    vec4 totalDiffuse = vec4(0.0);
    vec4 totalSpecular = vec4(0.0);

    for (int i = 0; i < 24; ++i) {
       vec3 lightDirection;
       float fDistance;
       lightDirection = normalize(LightPosistions[i] - FragPos);
       fDistance = length(LightPosistions[i] - FragPos);
       
       if (fDistance <= 8.0f) {
           float attenuation = 1.0 / (1.0 + 0.01f * fDistance + 0.001f * fDistance * fDistance);

           float normalDotLight = max(0.0, dot(normal, lightDirection));
           vec3 reflection = reflect(-lightDirection, normal);
           float specularReflectionDotView = max(0.0, dot(reflection, viewDirection));

           totalAmbient += vec4(0.3451f, 0.1436f, 0.0f, 1.0f) * vec4(0.8863f, 0.8863f, 0.8863f, 1.0f) * baseColor;
           totalDiffuse += vec4(0.3451f, 0.1436f, 0.0f, 1.0f)  * vec4(0.4902f, 0.4902f, 0.4902f, 1.0f) * normalDotLight * attenuation * baseColor;
           totalSpecular += vec4(0.3451f, 0.1436f, 0.0f, 1.0f)  * vec4(0.3686f, 0.3686f, 0.4706f, 1.0f) * pow(specularReflectionDotView, 5.08f) * attenuation;
       }
   }

    vec3 lightDirection = normalize(LightDirection);
    float normalDotLight = dot(normal,lightDirection); 
   
    vec3 reflection = reflect(-lightDirection, normal); 
   
    float specularReflectionDotView = max( 0.0, dot( reflection, viewDirection ) );
   
    totalAmbient += vec4(0.8863f, 0.8863f, 0.8863f, 1.0f) * baseColor; 
    totalDiffuse += vec4(0.4902f, 0.4902f, 0.4902f, 1.0f) * normalDotLight * baseColor; 
    totalSpecular += vec4(0.3686f, 0.3686f, 0.4706f, 1.0f) * (pow(specularReflectionDotView, 5.08f));

    Color = (totalAmbient + totalDiffuse + totalSpecular) * 1.0f;
    Color.a = 1.0f;
}