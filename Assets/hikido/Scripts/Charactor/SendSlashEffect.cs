using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSlashEffect : MonoBehaviour
{
    [SerializeField] GamaManagerSO _gameSO;

    private void Start()
    {
        Destroy(this.gameObject, _gameSO.effectlifeTime);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _gameSO.slashSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Enemy")
        {
            Debug.Log("ê⁄êGÇµÇƒÇ¢ÇÈÇÊ");
        }
    }

}
