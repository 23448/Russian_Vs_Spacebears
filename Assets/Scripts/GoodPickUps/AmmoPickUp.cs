using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickUp : MonoBehaviour {

    public static Transform Player;
    public static PickupText pickupText;

    [SerializeField] private float distance = 15.0f;
    [SerializeField] private float offset = 2.0f;

    public static Player_Shooting Shots;
    public static PlayerHealth Health;
    public static Player_Movement speed;

    public void Start()
    {
        Shots = Player.GetComponent<Player_Shooting>();
    }

    void Awake()
    {
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!pickupText)
        {
            pickupText = GameObject.Find("pickup text").GetComponent<PickupText>();
        }
        
    }

    void Update()
    {
        if (!Player)
            return;

        var currentDistance = Vector3.Distance(Player.position, transform.position);

        if (currentDistance < distance)
        {
            var screenpos = Camera.main.WorldToScreenPoint(transform.position);
            screenpos.y += offset;
            pickupText.ShowText(screenpos);
        }


        if (currentDistance < distance && Input.GetKeyDown("e"))
        {
            Debug.Log("Moan23");
            Shots.clips += 5;
            Destroy(gameObject);
        }
    }

   
}
