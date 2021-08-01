using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : OverworldSpawnZone
{
    // Start is called before the first frame update
    void Start()
    {
        OverworldData.Instance.InitialPlayerSpawn = gameObject.transform;
    }

}
