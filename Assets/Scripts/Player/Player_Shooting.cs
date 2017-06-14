using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour {

    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject gunEnd;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Rigidbody rigidbody;

    float dir;
    float timer;
    public float fireRate = 1F;
    private float nextFire = 0.0F;

    Animator anim;
    Light gunLight;
    float effectsDisplayTime = 0.1f;
    public AudioSource gunAudio;

    public float clips = 15;
    public float bulletsPerClip = 3;
    public float bulletsLeft = 0;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        bulletsLeft = bulletsPerClip;
    }

    public void FixedUpdate()
    {
        Reload();

        if (Input.GetMouseButton(0) && Time.time > nextFire && bulletsLeft != 0)
        {
            timer = Time.time;
            gunAudio.Play();
            bulletsLeft--;
            nextFire = Time.time + fireRate;
            Shoot();
            anim.SetTrigger("Shoot");
            rigidbody.AddForce(-Player.transform.forward * 400);
        }
    }


    public void Shoot()
    {
        if (Bullet)
        {
            for(int i = 0; i < 5; i++)
            Instantiate(Bullet, gunEnd.transform.position, transform.rotation);
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown("r") && clips > 0 && bulletsLeft < 3 )
        { 
                clips--;
                Debug.Log("Reload");
                bulletsLeft = bulletsPerClip;
                anim.SetTrigger("Reload");
        }
    }

    
}
