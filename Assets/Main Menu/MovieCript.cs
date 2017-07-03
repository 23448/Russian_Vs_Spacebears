using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MovieCript : MonoBehaviour {

    VideoPlayer movie;
    public VideoClip clip;
    public VideoPlayer otherPlayer;

    [SerializeField] bool isLoop = false;

    public void Start()
    {
        movie = GetComponent<VideoPlayer>();
        movie.loopPointReached += Movie_loopPointReached;
    }

    private void Movie_loopPointReached(VideoPlayer source)
    {
        if(!isLoop)
        {
            gameObject.SetActive(false);
        }
        
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!otherPlayer)
                return;

            otherPlayer.gameObject.SetActive(true);
            otherPlayer.loopPointReached += StartGame;
         }

    }

    private void StartGame(VideoPlayer source)
    {
        source.Pause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("rvs");
    }
}
