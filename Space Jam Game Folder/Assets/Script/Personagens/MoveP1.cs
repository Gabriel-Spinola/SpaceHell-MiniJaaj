using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveP1 : MonoBehaviour
{
    public float speed;
    public float forceJump;

    private Rigidbody2D rig;
    public bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        Vector3 Mov = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position = transform.position + Mov * Time.deltaTime * speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if(Input.GetAxis("Horizontal") < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

        }
    }
    void Jump()
    {
        if((Input.GetKeyDown(KeyCode.W)) && (jumping))
        {
            jumping = false;
            rig.AddForce(new Vector2(0f,forceJump), ForceMode2D.Impulse);
        }       
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == 7)
        {
            jumping = true;
        }           
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            jumping = false;
        }   

    }
}