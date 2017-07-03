using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo_Count : MonoBehaviour {

    [SerializeField] Text Left;
    Player_Shooting Shots;

    private void Start()
    {
        Shots = GetComponent<Player_Shooting>();
    }

    private void Update()
    {
        if (Shots && Left)
            Left.text = string.Format("{0}/{1}", Shots.bulletsLeft, Shots.clips);
    }
}
