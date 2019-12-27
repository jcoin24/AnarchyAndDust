using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    public GameObject shopPanel;
    public SpawnManager state;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && (state.state == SpawnManager.SpawnState.COUNTING))
        {
            OpenShop();
        }
    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

