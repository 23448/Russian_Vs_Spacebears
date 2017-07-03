using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScript : MonoBehaviour {

    public GameObject Canvas;

    public void Resume()
    {
        Canvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
