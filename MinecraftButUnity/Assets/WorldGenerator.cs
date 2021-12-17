using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    
    GameObject testCube;
    // Start is called before the first frame update

    void Start()
    {
        testCube = new GameObject("testCube");
        testCube.AddComponent<MeshRenderer>();
        testCube.AddComponent<MeshFilter>();
        testCube.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Cube");
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
