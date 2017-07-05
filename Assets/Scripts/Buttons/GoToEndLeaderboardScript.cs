using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToEndLeaderboardScript : MonoBehaviour {

    public GameObject Canvas;
    public GameObject EndCanvas;

     void Start()
    {
        EndCanvas.gameObject.SetActive(false);
        Canvas.gameObject.SetActive(true);
    }

    public void Go () {
        EndCanvas.gameObject.SetActive(true);
        Canvas.gameObject.SetActive(false);
	}
}
