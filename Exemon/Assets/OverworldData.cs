using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldData : MonoBehaviour
{
    public static OverworldData Instance { get; private set; }

    public List<OverworldExemon> OverworldEntities = new List<OverworldExemon>();

    [System.Serializable]
    public class EntityPosition { GameObject entity; Vector3 position; }

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

    public void SaveWorldState()
    {

    }
    
    public void ApplyWorldState()
    {

    }


}
