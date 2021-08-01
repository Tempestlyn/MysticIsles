using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    public GameObject Sandblock;
    public GameObject Grassblock;
    public GameObject SandRamp;
    public GameObject GrassRamp;

    public int totalBaseBlockCount;
    public GameObject LastPlacedBlock;

    private int BaseBlockLimit;
    public IslandSize islandSize;
    public Biom biom;
    private void Start()
    {

    }


    public void GenerateGrassBlock(Vector3 position) 
    {
        
    }

    public void GenerateStartingBlock(GameObject block)
    {
        Instantiate(Sandblock, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        totalBaseBlockCount += 1;
    }
}

public enum IslandSize
{
    small,
    medium,
    large,
    giant
}
public enum Biom
{
    Forest,
    Island,
}

