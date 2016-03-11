attribute vec3 aVertexPosition;
attribute vec2 aVertexUV;
attribute vec3 aVertexNormal;

uniform mat4 uPMatrix; /* perspectiveView matrix */
uniform mat4 uVMatrix; /* view matrix */
uniform mat4 uOMatrix; /* object matrix */

uniform float round;
uniform float edge;

uniform float time;
varying vec2 vUV;
varying vec3 vNormal;
varying float d;
varying vec3 HV;
varying vec3 VP;
varying float r;
varying float e;

const vec3 LightPosition = vec3(0., 0., 4.);

void main(void) {
   
	vec3 ecPos = (uPMatrix * uVMatrix * uOMatrix * vec4(aVertexPosition, 1.0)).xyz;
	vec3 eye = -normalize(ecPos);
	
	//vector for surface to light position
	VP = vec3(LightPosition - ecPos);
	//distance from light to surface
	d = length(VP);
	VP = normalize(VP);
	HV = normalize(VP + eye);
   
   gl_Position = uPMatrix * uVMatrix * uOMatrix * vec4(aVertexPosition, 1.0);
   vUV = aVertexUV;
   vNormal = (uPMatrix * uVMatrix * uOMatrix * vec4(aVertexNormal, 1.0)).xyz;
   r = round;
   e = edge;

}