using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float hAxis;
    private float vAxis;

    private Rigidbody rb;

    public float speed;

    private Vector3 mov;

    private Animator anim;

    private bool isSneaking = false;

    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSneaking = true;
        }
        else {
            isSneaking = false;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Jump());
        }

        anim.SetBool("isSneaking", isSneaking);

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hAxis, 0f, vAxis).normalized;
        mov = dir * speed * Time.deltaTime;
        if (!isSneaking) {
            mov *= 2f;
        }
        if (dir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.25f);
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }
        
        
        
    }

    IEnumerator Jump() {
        anim.SetTrigger("Jump");
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce);
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + mov);
    }
}
