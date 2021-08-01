using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldData : MonoBehaviour
{
    public static OverworldData Instance { get; private set; }
    public List<BaseBattleEntity> ExistingOverworldEntities = new List<BaseBattleEntity>();
    public List<SpawnZoneData> ActiveSpawnZones = new List<SpawnZoneData>();
    public List<SpawnZoneData> PossibleSpawnZones = new List<SpawnZoneData>();
    public List<GameObject> LandPrefabs = new List<GameObject>();
    public int LandPrefabToGenerate;
    public int NumberOfInitialSpawnZones;
    public BaseBattleEntity Player;
    //[System.Serializable]
    //public class EntityPosition { GameObject entity; Vector3 position; }
    public Transform InitialPlayerSpawn;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        CreateIsland();
        //TODO: update player stats when save/load is finished

        
    }



    public void SaveWorldState()
    {
        for(var i = 0; i < ExistingOverworldEntities.Count; i++)
        {
            ExistingOverworldEntities[i].WorldSpawnPosition = ExistingOverworldEntities[i].TempOverworldGameobject.transform.position;
        }
    }
    
    public void ApplyWorldState()
    {
        Instantiate(LandPrefabs[LandPrefabToGenerate], transform.position, transform.rotation);
        for (var i = 0; i < ExistingOverworldEntities.Count; i++)
        {
            Instantiate(ExistingOverworldEntities[i].OverworldEntity, ExistingOverworldEntities[i].WorldSpawnPosition, ExistingOverworldEntities[i].OverworldEntity.transform.rotation);
        }
    }

    public void CreateIsland()
    {
        var landToGenerate = Random.Range(0, LandPrefabs.Count - 1);
        var land = Instantiate(LandPrefabs[landToGenerate], transform.position, transform.rotation);
        LandPrefabToGenerate = landToGenerate;

        SpawnEntity(Player, land.GetComponent<LandPrefabScript>().PlayerSpawn.transform.position);
        for (var i = 0; i < land.GetComponent<LandPrefabScript>().BattleEntitySpawns.Count; i++)
        {
            var spawnData = PossibleSpawnZones[Random.Range(0, PossibleSpawnZones.Count)];
            spawnData.position = land.GetComponent<LandPrefabScript>().BattleEntitySpawns[i].transform.position;
            ActiveSpawnZones.Add(spawnData);
        }


        for (var i = 0; i < ActiveSpawnZones.Count; i++)
        {
            SpawnEntity(ActiveSpawnZones[i].Entity, ActiveSpawnZones[i].position);
        }

    }

    
    public void SpawnEntity(BaseBattleEntity SpawningEntity, Vector3 position)
    {
        SpawningEntity.WorldSpawnPosition = position;
        ExistingOverworldEntities.Add(SpawningEntity);
        var entity = Instantiate(SpawningEntity.OverworldEntity, position, SpawningEntity.OverworldEntity.transform.rotation);
        entity.GetComponent<OverworldExemon>().Exemon = SpawningEntity;
        SpawningEntity.TempOverworldGameobject = entity;

    }

}

[System.Serializable]
public struct SpawnZoneData
{
    public BaseBattleEntity Entity;
    public float SpawnCoolDown;
    public Vector3 position;
    public SpawnZoneData(BaseBattleEntity entity, float SpawnCoolDown, Vector3 position)
    {
        this.Entity = entity;
        this.SpawnCoolDown = SpawnCoolDown;
        this.position = position;

    }
}