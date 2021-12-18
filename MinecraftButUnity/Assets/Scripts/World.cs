using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Transform player;
    public Vector3 spawnPosition;
    public Material material;
    public BlockType[] blockTypes;

    Chunck[,] chuncks = new Chunck[ChunckData.worldSizeInChuncks,ChunckData.worldSizeInChuncks];
    List<ChunckCoord> activeChuncks = new List<ChunckCoord>();
    ChunckCoord playerChunckCoord;
    ChunckCoord playerLastChunckCoord;
    private void Start() {

        spawnPosition = new Vector3((ChunckData.worldSizeInChuncks * ChunckData.width)/ 2f, ChunckData.height+5,(ChunckData.worldSizeInChuncks * ChunckData.width) / 2f);
        generateWorld();
        playerLastChunckCoord = getChunckCoordFromVector3(player.position);
        
    }
    private void Update() {
        playerChunckCoord = getChunckCoordFromVector3(player.position);
        if(!playerChunckCoord.Eqeuals(playerLastChunckCoord))
        {
            checkViewDistance();
        }
        
    }

    void generateWorld()
    {
        for(int x = (ChunckData.worldSizeInChuncks/2) - ChunckData.viewDistanceInChuncks; x < (ChunckData.worldSizeInChuncks/2) + ChunckData.viewDistanceInChuncks; x++)
        {
            for(int z = (ChunckData.worldSizeInChuncks/2) - ChunckData.viewDistanceInChuncks; z < (ChunckData.worldSizeInChuncks/2) + ChunckData.viewDistanceInChuncks; z++)
            {
                creatChunck(x,z);
            }
        }
        player.position = spawnPosition;
    }
    void creatChunck(int x, int z)
    {
        chuncks[x,z] = new Chunck(new ChunckCoord(x,z),this);
        activeChuncks.Add(new ChunckCoord(x,z));
    }

    bool isChunckInWorld(ChunckCoord c)
    {
        if(c.x > 0 && c.x < ChunckData.worldSizeInChuncks-1 && c.z > 0 && c.z < ChunckData.worldSizeInChuncks -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isVoxelInWorld(Vector3 pos)
    {
        if(pos.x >= 0 && pos.x < ChunckData.worldSizeInVoxels&& pos.y >= 0 && pos.y < ChunckData.height && pos.z >= 0 && pos.z < ChunckData.worldSizeInVoxels)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public  byte getVoxel (Vector3 pos)
    {
        if(!isVoxelInWorld(pos))
        {
            return 0;
        }
        if(pos.y <= 1)
        {
            // bed rock
            return 1;
        }
        else if (pos.y > 1 && pos.y < ChunckData.height-2)
        {
            return 2;
        }
        else if (pos.y == ChunckData.height-1)
        {
            return 4;
        }
        else
        {
            return 2;
        }

    }
    ChunckCoord getChunckCoordFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x/ChunckData.width);
        int z = Mathf.FloorToInt(pos.z/ChunckData.width);
        return new ChunckCoord(x,z);
    }
    void checkViewDistance()
    {
        List<ChunckCoord> previuslyActiveChuncks = new List<ChunckCoord>(activeChuncks);
        ChunckCoord coord = getChunckCoordFromVector3(player.position);
        for(int x = coord.x - ChunckData.viewDistanceInChuncks; x < coord.x + ChunckData.viewDistanceInChuncks; x++)
        {
            for(int z = coord.z - ChunckData.viewDistanceInChuncks; z < coord.z + ChunckData.viewDistanceInChuncks; z++)
            {
                if(isChunckInWorld(new ChunckCoord(x,z)))
                {
                    if(chuncks[x,z] == null)
                    {
                        // cunck has not been generated
                        creatChunck(x,z);
                    }
                    else if (!chuncks[x,z].isActive)
                    {
                        chuncks[x,z].isActive = true;
                        activeChuncks.Add(new ChunckCoord(x,z));
                    }
                }
                for(int i = 0; i < previuslyActiveChuncks.Count; i++)
                {
                    if(previuslyActiveChuncks[i].Eqeuals(new ChunckCoord(x,z)))
                    {
                        previuslyActiveChuncks.RemoveAt(i);
                    }
                }
            }

        }

        foreach (ChunckCoord c in previuslyActiveChuncks)
        {
            chuncks[c.x,c.z].isActive = false;
        }

    }
}






[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;
    [Header ("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;

    public int getTextureID(int faceIndex)
    {
        switch(faceIndex)
        {
            case 0: 
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3: 
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                return -1;
        }

    }
}
