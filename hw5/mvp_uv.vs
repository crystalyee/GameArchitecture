attribute vec3 aVertexPosition;
attribute vec2 aVertexUV;
attribute vec3 aVertexNormal;
uniform mat4 uPMatrix; /* perspective matrix */
uniform mat4 uVMatrix; /* view matrix */
uniform mat4 uOMatrix; /* object matrix */

void main(void) {
   gl_Position = uPMatrix * uVMatrix * uOMatrix * vec4(aVertexPosition, 1.0);
}