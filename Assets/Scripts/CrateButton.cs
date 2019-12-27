using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrateButton : MonoBehaviour {

    // Reference to text ui elements
    public Text textName;
    public Text cost;

    // Reference to the turrets that are being used
    public TurretScript TurretPrefabToSpawn;

    // Reference to the players score
    public PlayerScore score;

    public GameObject crate;

    // Use this for initialization
    void Start () {
        SetButton();
	}

    void SetButton()
    {
        textName.text = TurretPrefabToSpawn.turretName;
        cost.text = "$" + TurretPrefabToSpawn.cost;
    }

    public void OnClick()
    {
        if (score.currentScore >= TurretPrefabToSpawn.cost)
        {
            score.MinusScore(TurretPrefabToSpawn.cost);
            if(TurretPrefabToSpawn != null)
            {
                Instantiate(TurretPrefabToSpawn, crate.transform.position + TurretPrefabToSpawn.SpawnOffset, Quaternion.Euler(Vector3.zero));
            }
            Destroy(crate);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
