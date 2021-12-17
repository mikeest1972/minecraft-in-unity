using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunck : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    private int triangleVertexIndex = 0;
    void Start()
    {
        transform.position = Vector3.zero;
        addChunckData();
        addMesh();
        
    }
    private void addMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

    }
    private void addChunckData()
    {
        for(int w = 0; w<ChunckData.width; w++)
        {
            for(int l = 0; l<ChunckData.length;l++)
            {
                Vector3 shift = new Vector3(w,0,l);
                addBlockData(shift);
            }
        }
    }
    private void addBlockData(Vector3 shift)
    {
        // add 1 block data
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 6; j++)
            {
                
                int currentVertexIndex = BlockData.blockTriangles[i,j];
                Vector3 currentVertex = BlockData.verticies[currentVertexIndex] + shift;
                vertices.Add(currentVertex);
                triangles.Add(triangleVertexIndex);
                triangleVertexIndex++;
                uvs.Add(BlockData.grassBlockUvs[i,j]);


            }
        }
    }
    
}
