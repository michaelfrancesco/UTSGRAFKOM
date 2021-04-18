#version 330 core

layout(location = 0) in vec3 aPosition;

uniform mat4 transform;
uniform mat4 view;
uniform mat4 projection;

// penambahan texture coordinates

layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

void main(void)
{
    
    texCoord = aTexCoord;

    gl_Position = vec4(aPosition, 1.0) * transform * view * projection ;	
}