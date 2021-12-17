using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockData
{
    // constins all the graphical data
	public static readonly int TextureAtlasSizeInBlock = 4;
	public static float NormalizedBlockTextureSize{
		get{ return 1f/(float)TextureAtlasSizeInBlock;}
	}
	    public static readonly Vector3[] verticies = new Vector3[8] {
        new Vector3(0.0f, 0.0f, 0.0f),
		new Vector3(1.0f, 0.0f, 0.0f),
		new Vector3(1.0f, 1.0f, 0.0f),
		new Vector3(0.0f, 1.0f, 0.0f),
		new Vector3(0.0f, 0.0f, 1.0f),
		new Vector3(1.0f, 0.0f, 1.0f),
		new Vector3(1.0f, 1.0f, 1.0f),
		new Vector3(0.0f, 1.0f, 1.0f),
    };

	public static readonly int[,] blockTriangles = new int[6,4] {
		// map to index on the vertices array above

		{0, 3, 1, 2}, // Back Face
		{5, 6, 4, 7}, // Front Face
		{3, 7, 2, 6}, // Top Face
		{1, 5, 0, 4}, // Bottom Face
		{4, 7, 0, 3}, // Left Face
		{1, 2, 5, 6} // Right Face
	};

	public static readonly Vector3[] faceChecks = new Vector3[6]
	{
		new Vector3(0.0f, 0.0f, -1.0f),
		new Vector3(0.0f, 0.0f, 1.0f),
		new Vector3(0.0f, 1.0f, 0.0f),
		new Vector3(0.0f, -1.0f, 0.0f),
		new Vector3(-1.0f, 0.0f, 0.0f),
		new Vector3(1.0f, 0.0f, 0.0f)
	};
	public static readonly Vector2[] voxelUvs = new Vector2[4]
	{
		new Vector2(0.0f,0.0f),
		new Vector2(0.0f,1.0f),
		new Vector2(1.0f,0.0f),
		new Vector2(1.0f,1.0f)
	};
	public static readonly Vector2[,] grassBlockUvs = new Vector2[6,6]
	{
		// back face
		{new Vector2(0.5f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,1f)},
		// Front face
		{new Vector2(0.5f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,1f)},
		// Top face (diffrent)
		{new Vector2(0.75f,0.5f),
		new Vector2(0.75f,0.75f),
		new Vector2(1f,0.5f),
		new Vector2(1f,0.5f),
		new Vector2(0.75f,0.75f),
		new Vector2(1f,0.75f)},
		// Bottom face (diffrent)
		{new Vector2(0.25f,0.75f),
		new Vector2(0.25f,1f),
		new Vector2(0.5f,0.75f),
		new Vector2(0.5f,0.75f),
		new Vector2(0.25f,1f),
		new Vector2(0.5f,1f)},
		// Left face
		{new Vector2(0.5f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,1f)},
		// Right face
		{new Vector2(0.5f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.75f,0.75f),
		new Vector2(0.5f,1f),
		new Vector2(0.75f,1f)}

	};

}
