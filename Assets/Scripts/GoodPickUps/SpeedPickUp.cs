using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickUp : MonoBehaviour {
    [SerializeField]
    private float distance = 15.0f;
    [SerializeField]
    private float offset = 2.0f;


    public float cooldownDestroy = 35f;
    

    static Player_Movement speed;

    public void Start()
    {
        speed = AmmoPickUp.Player.GetComponent<Player_Movement>();
    }

     void Awake()
    {
        if (!AmmoPickUp.Player)
            AmmoPickUp.Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!AmmoPickUp.pickupText)
        {
            AmmoPickUp.pickupText = GameObject.Find("pickup text").GetComponent<PickupText>();
        }

    }

     void Update()
    {
        if (!AmmoPickUp.Player)
            return;

        var currentDistance = Vector3.Distance(AmmoPickUp.Player.position, transform.position);

        if (currentDistance < distance)
        {
            var screenpos = Camera.main.WorldToScreenPoint(transform.position);
            screenpos.y += offset;
            AmmoPickUp.pickupText.ShowText(screenpos);

        }

        if (currentDistance < distance && Input.GetKeyDown("e"))
        {
            speed.SetSpeedPowerUp();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, cooldownDestroy);
        }

    }

}
