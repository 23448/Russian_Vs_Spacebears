using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public Text scoreText;

    public float score = 0;

    public void Start()
    {
        scoreText.text = "Score : " + score;
    }

    public void Update()
    {
        scoreText.text = "Score : " + score;
    }

}
