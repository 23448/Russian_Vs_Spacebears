using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAmmo : MonoBehaviour {

    [SerializeField]
    private float distance = 15.0f;
    [SerializeField]
    private float offset = 2.0f;
    [SerializeField]
    private float cooldown = 2.0f;

    float timer;
    private float nextDrop = 0.0F;
    bool isCoollingDown = false;

    static Player_Shooting clips;
    static PlayerHealth Health;

    public void Start()
    {
        Health = AmmoPickUp.Player.GetComponent<PlayerHealth>();
        clips = AmmoPickUp.Player.GetComponent<Player_Shooting>();

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

        if (currentDistance < distance && !isCoollingDown)
        {
            var screenpos = Camera.main.WorldToScreenPoint(transform.position);
            screenpos.y += offset;
            AmmoPickUp.pickupText.ShowText(screenpos);
        }

        if (currentDistance < distance && Input.GetKeyDown("e") && !isCoollingDown)
        {
            timer = Time.time;
            clips.clips += 1;
            nextDrop = Time.time + cooldown;
            isCoollingDown = true;
        }
        if (isCoollingDown)
        {
            if(Time.time > timer + cooldown)
            {
                isCoollingDown = false;
            }
        }
    }
}
