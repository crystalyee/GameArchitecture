Opening the HTML file directly off disk will not work because it's loading external data, which is a security risk. The HTML must be loaded as if you're hosting a webserver.

I do this with a Python built-in module that I run from a terminal after I've changed to the directory with the files in question:
python -m SimpleHTTPServer

then you can open a browser to http://127.0.0.1:8000

You will get an error message that top_verts is not defined. Your assignment is as follows:

parse top.json to get the mesh data for the crade lid

define the functions top_verts() and top_indices() to return the appropriate vertex array and index array

create a second object, parse botton.json to get the mesh data for the crade bottom, and define bottom_verts() and bottom_indices() to return the appropriate vertex array and index array

Set the getAttribLocation() function calls to take in a different set of vertex attributes than our cube. Namely, position, UVs, and normal (as opposed to the cube, which was position, normal, color)

In drawObject(), fix the vertexAttribPointer() calls to set the correct correspondence with our new vertex structure and the attributes that the vertex shader are expecting (vec3, vec2, vec3)