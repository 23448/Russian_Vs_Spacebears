using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

    [SerializeField]
    private float distance = 15.0f;
    [SerializeField]
    private float offset = 2.0f;
    [SerializeField]
    private Text endingText;

    float timer;
    public float textTime = 2F;
    private float destroyText = 0.0F;

    public static Transform Player;
    public PickupText endText;
    WaveSpawner wave;

    void Awake()
    {
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (endText)
        {
            endText = GameObject.Find("endText").GetComponent<PickupText>();
        }

        wave = GetComponent<WaveSpawner>();
    }

    public void Update()
    {
        if (WaveSpawner.nextWave >= 10)
        {
            endingText.gameObject.SetActive(true);
            timer = Time.time;

            if (!Player)
                return;


            textTime = Time.time + destroyText;

            if(Time.time > destroyText)
            {
                endingText.gameObject.SetActive(false);
            }

            var currentDistance = Vector3.Distance(Player.position, transform.position);

            if (currentDistance < distance)
            {
                var screenpos = Camera.main.WorldToScreenPoint(transform.position);
                screenpos.y += offset;
                endText.ShowText(screenpos);
            }

            if (currentDistance < distance && Input.GetKeyDown("e"))
            {
                Debug.Log("End");
            }
        }
    }
}
