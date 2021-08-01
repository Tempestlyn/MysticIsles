using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatComponent : MonoBehaviour
{
    public int CurrentHeatValue;
    public int AppliedHeatValue;
    public int MeltThreshold;
    public int FireThreshold;
    public GameObject MeltPrefab;
    public GameObject FirePrefab;
    public bool TakesFireDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HeatComponent>())
        {
            collision.gameObject.GetComponent<HeatComponent>().ApplyHeat(AppliedHeatValue);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HeatComponent>())
        {
            collision.gameObject.GetComponent<HeatComponent>().ApplyHeat(AppliedHeatValue);
        }
    }

    public void ApplyHeat(int heat)
    {
        CurrentHeatValue += heat;
        if (CurrentHeatValue >= FireThreshold)
        {
            SetOnFire();
        }
        if (CurrentHeatValue <= MeltThreshold)
        {
            Melt();
        }
    }

    public void SetOnFire()
    {

    }

    public void Melt()
    {
        Instantiate(MeltPrefab, transform);
    }
}
