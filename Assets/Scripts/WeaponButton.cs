using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public PlayerShooting playerShooting;
    public int weaponNumber;

    public Text nameWeapon;
    public Text cost;
    public Text description;

    public PlayerScore score;

    //private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
        SetButton();
    }

    void SetButton()
    {
        string costString = playerShooting.weapons[weaponNumber].cost.ToString();
        nameWeapon.text = playerShooting.weapons[weaponNumber].name;
        cost.text = "$" + playerShooting.weapons[weaponNumber].cost;
        description.text = playerShooting.weapons[weaponNumber].description;
    }

    public void OnClick()
    {
        if (score.currentScore >= playerShooting.weapons[weaponNumber].cost)
        {
            score.MinusScore(playerShooting.weapons[weaponNumber].cost);
            playerShooting.currentWeapon = weaponNumber;
            playerShooting.SetWeapon(weaponNumber);
        }
        else
        {
            //source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}