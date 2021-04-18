#version 330

out vec4 outputColor;

in vec2 texCoord;

// A sampler2d is the representation of a texture in a shader.
// Each sampler is bound to a texture unit (texture units are described in Texture.cs on the Use function)
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
}