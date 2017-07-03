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

    EnemyAttack enemyAttack;
    PlayerScore playerScore;
    Animator anim;
    CapsuleCollider capsuleCollider;
    private float lastKnockbakTime;
    private float minKnoackback = 0.08f;


    bool isDead;
    bool isSinking;
    bool knockback = false;
    float knockbackVelocity = 0;
    const float wantedKnockbackVel = 0.4f;
    const float knockbackDecayFactor = 0.95f;


    void Awake()
    {
        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAttack = GetComponent<EnemyAttack>();
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

        enemyAttack.canAttack = false;
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

    private void Update()
    {
        if(knockback)
        {
            transform.Translate(-transform.forward * knockbackVelocity, Space.World);
            knockbackVelocity *= knockbackDecayFactor;
            if (knockbackVelocity < 0.1f)
                knockback = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            TakeDamage(10, transform.position);
            if(Time.time > lastKnockbakTime + 1)
            { 
                knockback = true;
                lastKnockbakTime = Time.time;
                knockbackVelocity = wantedKnockbackVel;
            }
            anim.SetTrigger("GetHit");
        }
    }
}
