using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DetachedSoundScript : MonoBehaviour {

    public AudioClip ClipToPlay = null;

	// Use this for initialization
	void Start () {
        if (ClipToPlay)
        {
            AudioSource AS = GetComponent<AudioSource>();
            AS.clip = ClipToPlay;
            AS.Play();
            Destroy(gameObject, AS.clip.length);
        }
        else
            Destroy(gameObject);
	}
}
