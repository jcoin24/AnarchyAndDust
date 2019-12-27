using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObject : ScriptableObject
{

    public string weaponName = "Weapon name here";
    public int cost = 50;
    public string description;

    public float fireRate = 0.15f;
    public int damage = 10;
    public float range = 100;

    public AudioClip gunAudio;
    public Light gunLight;
    public Material gunLine;

    public Mesh gunMesh;

}
