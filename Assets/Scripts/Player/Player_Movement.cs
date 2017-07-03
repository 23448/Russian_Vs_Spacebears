using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    Animator anim;
    Vector3 movement;
    Rigidbody Rigid;

    public float speed = 6f;
    public float runspeed = 10f;
    private float currentSpeed;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
        currentSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
        Animating2(h, v);

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);

        movement = movement.normalized * currentSpeed * Time.deltaTime;

        Rigid.MovePosition(transform.position + movement);
    }

    public void SetSpeedPowerUp()
    {
        StartCoroutine(Interna_SpeedPowerUp());
    }

    private IEnumerator Interna_SpeedPowerUp()
    {
        speed += 4;
        yield return new WaitForSeconds(4);
        speed -= 4;
    }

    void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            LookAt(hit.point);
            Debug.DrawRay(ray.origin, ray.direction * 100);
        }
    }

    public void LookAt(Vector3 point)
    {
        transform.LookAt(point);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y);
    }


    void Animating(float h, float v)
    {
        bool walking = v != 0f;
        if(anim)
            anim.SetBool("IsWalking", walking);
    }
    void Animating2(float h, float v)
    {
        bool wallkingRL = h != 0f;
        if (anim)
            anim.SetBool("IsWalkingRL", wallkingRL);
    }
}
