using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseExemon[] PartyExemon;
    private PlayerMovement playerMovement;
    public GameObject playerCamera;
    public GameObject battleCamera;

    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();  
    }

    void OnCollisionEnter(Collision collision)
    {
        var exemon = collision.gameObject.GetComponent<OverworldExemon>();

        if (exemon != null)
        {

           


        }

    }

    public void EndBattle()
    {
        //playerMovement.canMove = true;
        playerCamera.SetActive(true);
        battleCamera.SetActive(false);
    }



    public void StartBattle(BaseExemon exemon)
    {

        //playerMovement.canMove = false;
        battleCamera.SetActive(true);
        playerCamera.SetActive(false);

    }



}
