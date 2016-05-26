//Attach this script to your Cube.
//After you change the scale of the Cube, either
//	Click the "Update Texture" button [if in edit mode], or...
//	Call reCalcCubeTexture() [if in runtime]

#if UNITY_EDITOR //prevents contents from compiling into the final build
using UnityEditor;
#endif

using UnityEngine;
using System.Collections;


public class ReCalcCubeTexture : MonoBehaviour 
{

	Vector3 currentScale = new Vector3();

	void Start()
	{
		currentScale = transform.localScale;   
	}

	public void reCalcCubeTexture()
	{
		currentScale = transform.localScale;

		//if still here, update the UV map on the mesh so the texture will repeat at the correct scale

		float length = currentScale.x;
		float width = currentScale.z;
		float height = currentScale.y;

		Mesh mesh;

		#if UNITY_EDITOR
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		string savedName = meshFilter.sharedMesh.name;
		Mesh meshCopy = ( Mesh ) Mesh.Instantiate( meshFilter.sharedMesh );
		mesh = meshFilter.mesh = meshCopy;
		#else
		mesh = GetComponent<MeshFilter>().mesh;
		#endif

		Vector2[] mesh_UVs = mesh.uv;

		int i, j;

		for (i = 0; i < mesh.triangles.Length; i+=3)
		{
			if (Mathf.Approximately(mesh.vertices [mesh.triangles [i]].x, mesh.vertices [mesh.triangles [i + 1]].x) &&
				Mathf.Approximately(mesh.vertices [mesh.triangles [i]].x, mesh.vertices [mesh.triangles [i + 2]].x)) {
				for (j = 0; j < 3; j++)
					mesh_UVs [mesh.triangles [i + j]] = new Vector2 (width * (mesh.vertices[mesh.triangles[i+j]].z+0.5f), height * (mesh.vertices[mesh.triangles[i+j]].y+0.5f));
			} else if (Mathf.Approximately(mesh.vertices [mesh.triangles [i]].y, mesh.vertices [mesh.triangles [i + 1]].y) &&
			           Mathf.Approximately(mesh.vertices [mesh.triangles [i]].y, mesh.vertices [mesh.triangles [i + 2]].y)) {
				for (j = 0; j < 3; j++)
					mesh_UVs [mesh.triangles [i + j]] = new Vector2 (width * (mesh.vertices[mesh.triangles[i+j]].z+0.5f), length * (mesh.vertices[mesh.triangles[i+j]].x+0.5f));
			} else {
				for (j = 0; j < 3; j++)
					mesh_UVs [mesh.triangles [i + j]] = new Vector2 (length * (mesh.vertices[mesh.triangles[i+j]].x+0.5f), height * (mesh.vertices[mesh.triangles[i+j]].y+0.5f));
			}
		}

		mesh.uv = mesh_UVs;
		#if UNITY_EDITOR
		mesh.name = savedName;
		#endif

		if ( GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode != TextureWrapMode.Repeat )
			GetComponent<Renderer>().sharedMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
	}
}


//Create Button to allow the Update while in Editor
#if UNITY_EDITOR
[CustomEditor( typeof( ReCalcCubeTexture ) )]
public class UpdateTextures : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ReCalcCubeTexture myScript = ( ReCalcCubeTexture ) target;
		if ( GUILayout.Button( "Update Texture" ) )
		{
			myScript.reCalcCubeTexture();
		}
	}
}
#endif
