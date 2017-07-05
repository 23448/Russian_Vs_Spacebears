using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour {

    [SerializeField] private float moveSpeedNormal = 5;
    [SerializeField] private float moveSpeedRunning = 9;
    [SerializeField] private float maxSprintWaitTime = 30;
    [SerializeField] private float sprintTime = 3;
    bool canSprint = false;
    float sprintWaitTime;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
   

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        canSprint = true; //Mathf.Floor(Random.Range(1, 3)) == 1;
        sprintWaitTime = Random.Range(0, maxSprintWaitTime);
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);

            if (canSprint)
            {
                sprintWaitTime -= Time.deltaTime;
                if(sprintWaitTime <= 0)
                {
                    nav.speed = moveSpeedRunning;
                    sprintTime -= Time.deltaTime;
                    anim.speed = 1.5f;
                    if (sprintTime <= 0)
                    {
                        nav.speed = moveSpeedNormal;
                        anim.speed = 1;
                        canSprint = false;
                    }
                }
            }
        }
        else
        {
            nav.enabled = false;
        }
    }

}
