using UnityEngine;

[CreateAssetMenu(fileName = "BiomeAttributes",menuName = "Minecraft/BiomAttributes")]
public class BiomeAttributes : ScriptableObject
{
    public string biomeName;

    
    public int solidGroundHeight; // below this hight everything is solid
    public int terrainHeight; // highest point on this terrain
    public float terrainScale; // to pass to perlin noise
    public Load[] loads;
}

// ores
[System.Serializable]
public class Load
{
    public string nodeName;
    public byte blockID;
    public int minHeight;
    public int maxHeight;
    public float sacle;
    public float threshold;
    public float noiseOffset;
}