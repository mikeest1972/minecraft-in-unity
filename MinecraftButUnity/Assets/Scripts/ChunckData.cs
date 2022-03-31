using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChunckData
{
    public static readonly int width = 16;
    public static readonly int length = 16;
    public static readonly int height = 128;
    public static readonly int worldSizeInChuncks = 100;
    public static int worldSizeInVoxels {
        get {return worldSizeInChuncks * width;}
    }

    public static readonly int viewDistanceInChuncks = 5;
}
