using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    private void Start()
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int face = 0; face < 6; face++)
        {
            
            VoxelData.BlockFace faceType = face switch
            {
                0 => VoxelData.BlockFace.Top, // Back
                1 => VoxelData.BlockFace.Side, // Front
                2 => VoxelData.BlockFace.Top,
                3 => VoxelData.BlockFace.Bottom,
                4 => VoxelData.BlockFace.Side, // Left
                5 => VoxelData.BlockFace.Side, // Right
                _ => VoxelData.BlockFace.Side
            };
            
            Vector2Int atlasCoord = VoxelData.GrassTextureCoords[faceType];
            float tileSize = VoxelData.NormalizedBlockTextureSize;
            
            for (int v = 0; v < 6; v++)
            {
                int triangleIndex = VoxelData.VoxelTriangles[face, v];
                vertices.Add(VoxelData.VoxelVerticies[triangleIndex]);
                triangles.Add(vertexIndex);

                Vector2 uv = v switch
                {
                    0 => new Vector2(0, 0),
                    1 => new Vector2(0, 1),
                    2 => new Vector2(1, 0),
                    3 => new Vector2(1, 0),
                    4 => new Vector2(0, 1),
                    5 => new Vector2(1, 1),
                    _ => Vector2.zero
                };
                
                uv = new Vector2(
                    (atlasCoord.x + uv.x) * tileSize,
                    (atlasCoord.y + uv.y) * tileSize
                );

                uvs.Add(uv);
                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        
    }

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();
        
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        
        var mat = Resources.Load<Material>("Materials/BlockMaterial");
        if (mat == null)
            Debug.LogError("BlockMaterial not found!");
        else
            meshRenderer.material = mat;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(15f, 30f, 45f) * Time.deltaTime);
    }
}
