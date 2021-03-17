using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldExemon : MonoBehaviour
{
    public BaseExemon Exemon;
    // Start is called before the first frame update
    

    public void Start()
    {
        Exemon.InitializeExemon();
        
    }

    public void Update()
    {

    }

    private void LateUpdate()
    {
        
        //transform.forward = new Vector3(transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
    }
    public void GetExemonStats()
    {
        
    }
}
