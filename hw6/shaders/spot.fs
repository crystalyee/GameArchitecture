precision mediump float;

varying vec2 vUV;
varying vec3 vNormal;
varying vec3 HV;
varying vec3 VP;
varying float d; // depth
varying float r;
varying float e;

uniform sampler2D colorMap;

const vec3 ambient = vec3(.1, 0.1, 0.1);
const vec3 diffuse = vec3(0.7, 0.7, 0.9);
const vec3 specular = vec3(0.7, 0.7, 0.7);
const float constantAttenuation = 1.0;
const float linearAttenuation = 0.0;
const float quadraticAttenuation = 0.0;
const float shininess = 50.0; //can be as high as 128 - bigger number = more concentrated bright spot
float spotCutoff = cos(radians(20.0)); //cosine of angle for spot cone
const vec3 LightDirection = vec3(0.0, 0.0, -1.0);
const float spotExponent = 1.0;

// superellipse parameters
float width = 0.3;
float height = 0.3;
float widthEdge = e;
float heightEdge = e;
float round = r;

// distance parameters
float far = d;
float farEdge = 0.2;




float ellipseShape(vec3 VP, float width, float height, float widthEdge, float heightEdge, float round)
{
    float a = width;
    float b = height;
    float A = width + widthEdge;
    float B = height + heightEdge;

    float exp1 = 2.0 / round;
    float exp2 = -1.0 * round / 2.0;

    float inner = a * b * pow( pow(b * VP.x, exp1) + pow(a * VP.y, exp1), exp2);
    float outer = A * B * pow( pow(B * VP.x, exp1) + pow(A * VP.y, exp1), exp2);
    return 1.0 - smoothstep(inner, outer, 1.0);
}

float distanceShape(float far, float farEdge, float d)
{
    return 1.0 - smoothstep(far, far + farEdge, d);
}


void main(void)
{
    float pf=0.0; //power factor
    float attenuation = 1.0;   
    float spotAttenuation = 0.1; //final spotlight attenuation factor
    float nDotVP = 0.0;
    float nDotHV = 0.0;
    //normals may have been denormalized by linear interpolation, so we'll normalize *again*
    vec3 n = normalize(vNormal);
    vec3 c = ambient;
    nDotVP = max(dot(n,normalize(-VP)),0.0);
    if (nDotVP > 0.)
    {
	
        attenuation *= ellipseShape(VP, width, height, widthEdge, heightEdge, round);
        attenuation *= distanceShape(far, farEdge, d);

        c += attenuation * (diffuse * nDotVP); 
        nDotHV = max(0.0, dot(n, HV));
        c += attenuation * specular * pow(nDotHV, shininess);
        
    }

	gl_FragColor = texture2D(colorMap, vUV)  * vec4(c, 1.0);
	//gl_FragColor = vec4(nDotVP,nDotVP,nDotVP,1.);
}