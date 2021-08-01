using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSpawnZone : MonoBehaviour
{
    public BaseBattleEntity EntityToSpawn;
    public float SpawnCoolDown;
    public int SpawnZoneID;
    // Start is called before the first frame update

    private void Start()
    {
        //OverworldData.Instance.ActiveSpawnZones.Add
        //OverworldData.Instance.CreateSpawnPoint(transform.position);
    }
    /*
    void Start()
    {
        var isActive = false;
        for(var i = 0; i < OverworldData.Instance.ActiveSpawnZones.Count; i++)
        {
            if(OverworldData.Instance.ActiveSpawnZones[i].ID == SpawnZoneID)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }

        if (!isActive)
        {

            var zone = new SpawnZoneData(SpawnZoneID, EntityToSpawn, SpawnCoolDown, transform.position);
            OverworldData.Instance.ActiveSpawnZones.Add(zone);
            OverworldData.Instance.SpawnEntity(zone.Entity, zone.position);
        }
    }

    

}

public struct SpawnZoneData
{
    public int ID;
    public BaseBattleEntity Entity;
    public float SpawnCoolDown;
    public Vector3 position;
    public SpawnZoneData(int ID, BaseBattleEntity entity, float SpawnCoolDown, Vector3 position)
    {
        this.ID = ID;
        this.Entity = entity;
        this.SpawnCoolDown = SpawnCoolDown;
        this.position = position;

    }

    */
}
