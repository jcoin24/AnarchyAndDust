using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public int startScore = 0;
    public int currentScore = 0;

    public Text text;

	// Use this for initialization
	void Start ()
    {
        currentScore = startScore;
	}

    private void Update()
    {
        text.text = "Score: $" + currentScore;
    }

    public void MinusScore(int num)
    {
        

        if((currentScore - num) >= 0)
        {
            Debug.Log("Subtracting: " + num);
            currentScore -= num;

        }
        else
        {
            currentScore = 0;
        }
    }

    public void AddScore(int num)
    {
        currentScore += num;
    }
}
