using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float spread = 0.05f;

    //float bulletDamage = 10;
    private Rigidbody _rigidBody;
    float bulletLife = 0.85f;
    Vector3 Direction;

    public void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Direction = transform.forward;
        Direction.z += Random.Range(-spread, spread);
        Direction.x += Random.Range(-spread, spread);
        Direction.y = 0;
        Destroy(gameObject, bulletLife);
    }

    public void FixedUpdate()
    {
         Vector3 velocity = Direction * _speed * Time.fixedDeltaTime;
         _rigidBody.MovePosition(_rigidBody.position + velocity);
    }
}
