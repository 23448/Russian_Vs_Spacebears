using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public float flashSpeed = 0.1f;
    public Image damgeImage;
    public Sprite hurtimage;

    Player_Movement playerMovement;
    Player_Shooting playerShooting;

    public AudioSource die;
    AudioSource Moan;

    public GameObject Test;
    Animator anim;

    bool damaged = false;
    bool IsDead = false;

    public void Awake()
    { 
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        playerMovement = GetComponent<Player_Movement>();
        playerShooting = GetComponent<Player_Shooting>();
        Moan = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (damaged)
        {
            damgeImage.sprite = hurtimage;
            damgeImage.color = Color.white;
        }
        else
        {
            var c = damgeImage.color;
            c.a -= flashSpeed * Time.deltaTime;
            damgeImage.color = c;
        }
        damaged = false;

    }

    void Death()
    {
        IsDead = true;
        die.Play();
        Moan.Stop();

        anim.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !IsDead)
        {
            Death();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Test"))
        {
            TakeDamage(20);
            Destroy(Test);
        }
    }
}
