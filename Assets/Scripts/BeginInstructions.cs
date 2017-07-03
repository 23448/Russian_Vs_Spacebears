using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginInstructions : MonoBehaviour {

    public GameObject Canvas;
    public GameObject hudCanvas;
    public AudioSource backgroundMusic;

    void Start () {
        Time.timeScale = 0f;
        hudCanvas.gameObject.SetActive(false);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            backgroundMusic.Play();
            Time.timeScale = 1f;
            Canvas.gameObject.SetActive(false);
            hudCanvas.gameObject.SetActive(true);
        }
	}
}
