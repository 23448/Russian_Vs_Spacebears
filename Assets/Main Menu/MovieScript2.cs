using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieScript2 : MonoBehaviour
{

    public VideoClip movie;

    public void Start()
    {
        movie = GetComponent<VideoClip>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            gameObject.SetActive(false);
        }
    }
}
