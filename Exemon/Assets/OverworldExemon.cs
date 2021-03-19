using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldExemon : MonoBehaviour
{
    public BaseExemon Exemon;
    // Start is called before the first frame update
    public Camera MainCamera;

    public void Start()
    {
        Exemon.InitializeExemon();
        MainCamera = Camera.main;
    }

    public void Update()
    {
        if (MainCamera != null)
            transform.rotation = new Quaternion(MainCamera.transform.rotation.x, MainCamera.transform.rotation.y, MainCamera.transform.rotation.z, MainCamera.transform.rotation.w);
    }

    private void LateUpdate()
    {
        
        //transform.forward = new Vector3(transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
    }
    public void GetExemonStats()
    {
        
    }


}
