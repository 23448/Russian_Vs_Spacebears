using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    public GameObject Bullet;
    public static Transform Player;

    public Transform[] items;

    PlayerScore playerScore;
    Animator anim;
    CapsuleCollider capsuleCollider;

    bool isDead;


    void Awake()
    {
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();
        playerScore = Player.GetComponent<PlayerScore>();

        currentHealth = startingHealth;
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;
        

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
            anim.SetTrigger("Dead");
        }
    }


    void Death()
    {
        isDead = true;

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Destroy(gameObject, 3f);

        var randomDrop = Random.Range(0, 10);
        if(randomDrop >= 5)
        {
            dropRandomItem();
        }

        playerScore.score += 10;
    }


    void dropRandomItem()
    {
        Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            TakeDamage(10, transform.position);
            anim.SetTrigger("GetHit");
        }
    }
}
