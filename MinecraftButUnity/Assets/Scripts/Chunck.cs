using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunck
{
    public ChunckCoord coord;
    GameObject chunckObject;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    private int triangleVertexIndex = 0;
    World world;
    byte [,,] voxelMap = new byte[ChunckData.width,ChunckData.height,ChunckData.width];
    // void Start()
    // {
    //     world = GameObject.Find("World").GetComponent<World>();
    //     transform.position = Vector3.zero;
    //     populateVoxelMap();
    //     addChunckData();
    //     addMesh();
        
    // }
    public Chunck(ChunckCoord c, World w)
    {
        coord = c;
        world = w;
        chunckObject = new GameObject();
        meshFilter = chunckObject.AddComponent<MeshFilter>();
        meshRenderer = chunckObject.AddComponent<MeshRenderer>();
        meshRenderer.material = world.material;
        chunckObject.transform.SetParent(world.transform);
        chunckObject.transform.position = new Vector3(coord.x * ChunckData.width,0f,coord.z * ChunckData.width);
        chunckObject.name = "Chunck " + coord.x + ", " + coord.z;
        //chunckObject.transform.position = Vector3.zero;
        populateVoxelMap();
        addChunckData();
        addMesh();

    }
    void populateVoxelMap()
    {
        for (int y = 0; y < ChunckData.height; y++)
        {
            for (int x = 0; x < ChunckData.width; x++)
            {
                for (int z = 0; z < ChunckData.width; z++)
                {
                    voxelMap[x,y,z] = world.getVoxel(new Vector3(x,y,z) + position);
                    
                }
            }
            
        }
    }

    public bool isActive
    {
        get {return chunckObject.activeSelf;}
        set {chunckObject.SetActive(value);}
    }

    public Vector3 position{
        get{return chunckObject.transform.position;}
    }
    bool isVoxelInChunck(int x, int y, int z)
    {
        if(x < 0 || x > ChunckData.width -1 || y < 0 || y > ChunckData.height -1 || z < 0 || z > ChunckData.width-1)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
    
    bool checkVoxel(Vector3 pos)
    {
        // checks if there is a voxel there
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);
        if(!isVoxelInChunck(x,y,z))
        {
            // handles the case where the index will be outside of the array 
            // -1 or anyting grater than the size of its
            return world.blockTypes[world.getVoxel(pos+position)].isSolid;
        }
        return world.blockTypes[voxelMap[x,y,z]].isSolid;
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
        for(int y = 0; y<ChunckData.height; y++)
        {
            for(int x = 0; x<ChunckData.width;x++)
            {
                for (int z = 0; z < ChunckData.width; z++)
                {
                    if(world.blockTypes[voxelMap[x,y,z]].isSolid)
                    {
                        Vector3 shift = new Vector3(x,y,z);
                        addBlockDataToChunck(shift);
                    }
                    
                }
                
            }
        }
    }
    private void addBlockDataToChunck(Vector3 pos)
    {
        // add 1 block data
        for(int i = 0; i < 6; i++)
        {
            if(!checkVoxel(pos+BlockData.faceChecks[i]))
            {
                byte blockID = voxelMap[(int)pos.x,(int)pos.y,(int)pos.z];
                vertices.Add(pos+BlockData.verticies[BlockData.blockTriangles[i,0]]);
                vertices.Add(pos+BlockData.verticies[BlockData.blockTriangles[i,1]]);
                vertices.Add(pos+BlockData.verticies[BlockData.blockTriangles[i,2]]);
                vertices.Add(pos+BlockData.verticies[BlockData.blockTriangles[i,3]]);
                addTexture(world.blockTypes[blockID].getTextureID(i)); // adds the texture to each fase
                triangles.Add(triangleVertexIndex);
                triangles.Add(triangleVertexIndex+1);
                triangles.Add(triangleVertexIndex+2);
                triangles.Add(triangleVertexIndex+2);
                triangles.Add(triangleVertexIndex+1);
                triangles.Add(triangleVertexIndex+3);
                triangleVertexIndex +=4;
                
            }
            
        }
    }
    void addTexture(int textureID)
    {
        // cool way of indexing the texture atlas
        // stone is 0 to coal 3
        // wood planks 4 ... and so on
        float y = textureID/ BlockData.TextureAtlasSizeInBlock;
        float x = textureID - (y * BlockData.TextureAtlasSizeInBlock);

        x *= BlockData.NormalizedBlockTextureSize;
        y *= BlockData.NormalizedBlockTextureSize;

        y = 1f - y - BlockData.NormalizedBlockTextureSize;
        uvs.Add(new Vector2(x,y));
        uvs.Add(new Vector2(x,y+ BlockData.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x+ BlockData.NormalizedBlockTextureSize,y));
        uvs.Add(new Vector2(x+ BlockData.NormalizedBlockTextureSize,y+ BlockData.NormalizedBlockTextureSize));
    }
    
}



public class ChunckCoord
{
    // positno of the chunck
    public int x;
    public int z;

    public ChunckCoord(int xC, int zC)
    {
        x = xC;
        z = zC;
    }

    public bool Eqeuals(ChunckCoord other)
    {
        if(other == null)
        {
            return false;
        }
        else if (other.x == x && other.z == z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    
}