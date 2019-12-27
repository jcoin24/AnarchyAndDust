using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] roundMusic;
    public AudioSource AS;
    int clipIndex;
    int roundNo;

    private void Awake()
    {
        
        AS = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        
        clipIndex = 0;
        SwitchToClip(clipIndex);
        AS.Play();
    }
	
	// Update is called once per frame
	void Update () {
        roundNo = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().healthRound;
        if (clipIndex <= roundNo-1)
        {
            clipIndex = (roundNo%13);
            SwitchToClip(clipIndex);
            
        }
        LoopIt();
	}

    void SwitchToClip(int clipNo)
    {
        if(clipNo < roundMusic.Length-1)
        AS.clip = roundMusic[clipNo];
    }

    void LoopIt()
    {
        if (!AS.isPlaying)
        {
            AS.Play();
        }
    }
}
