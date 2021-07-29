using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldExemon : MonoBehaviour
{
    public BaseBattleEntity Exemon;
    // Start is called before the first frame update
    public Camera MainCamera;
    public List<Item> DroppableItems;

    public void Start()
    {
        //Exemon.InitializeExemon();
        MainCamera = Camera.main;
    }

    public void LateUpdate()
    {
         
        if (MainCamera != null) {
            transform.rotation = MainCamera.transform.rotation;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }



}
