using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour {

    [SerializeField]
    private float distance = 15.0f;
    [SerializeField]
    private float offset = 2.0f;

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
            if (!Player)
                return;

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
