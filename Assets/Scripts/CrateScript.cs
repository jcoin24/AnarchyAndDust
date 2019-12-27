using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour {

    public TurretScript TurretPrefabToSpawn;

    bool IsPlayerInRange = false;

    public void Update()
    {
        if (Input.GetButtonDown("PurchaseCrate"))
        {
            if (IsPlayerInRange)
            {
                Instantiate(TurretPrefabToSpawn, transform.position + TurretPrefabToSpawn.SpawnOffset, Quaternion.Euler(Vector3.zero));
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayerInRange)
            if (other && other.CompareTag("player"))
                IsPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayerInRange)
            if (other && other.CompareTag("player"))
                IsPlayerInRange = false;
    }
}