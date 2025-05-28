using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
	public static readonly int TextureAtlasSizeInBlocks = 4;
	public static float NormalizedBlockTextureSize => 1f / TextureAtlasSizeInBlocks;

	public enum BlockFace { Top, Bottom, Side }
	public static readonly Dictionary<BlockFace, Vector2Int> GrassTextureCoords = new() 
	{
		{ BlockFace.Top, new Vector2Int(0, 3) },     // e.g., Grass_Top at (0,3)
        { BlockFace.Bottom, new Vector2Int(0, 2) },  
        { BlockFace.Side, new Vector2Int(0, 1) }   
    };

    public static readonly Vector3[] VoxelVerticies = new Vector3[8]
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),
    };
    
    public static readonly int[,] VoxelTriangles = new int[6, 6]
    {
        {0, 3, 1, 1, 3, 2}, // Back Face.
        {5, 6, 4, 4, 6, 7}, // Front Face.
        {3, 7, 2, 2, 7, 6}, // Top Face.
        {1, 5, 0, 0, 5, 4}, // Bottom Face.
        {4, 7, 0, 0, 7, 3}, // Left Face.
        {1, 2, 5, 5, 2, 6}, // Right Face.
    };
}
