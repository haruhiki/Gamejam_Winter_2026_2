using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponColision : MonoBehaviour
{
    public Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
        if(!collider) { Debug.LogWarning("コライダーなし"); }

        SetCollisionActive(false);

    }

    public void SetCollisionActive(bool isActive) 
    {
        if(collider != null) { collider.enabled = isActive; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){ Debug.Log("当たっている。"); }
    }
}
