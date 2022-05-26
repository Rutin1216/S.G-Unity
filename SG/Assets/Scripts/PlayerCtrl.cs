using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{
    public int MSpeed;
    public int JForce;
    
    private Vector3 Pos;
    private float Horizontal;
    private Rigidbody2D rigid2D;
    private Animator anim;
    private SpriteRenderer sprender;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Jump();
    }
    void FixedUpdate()
    {
        
        
    }
    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Pos = transform.position;
        Pos.x += Horizontal * Time.deltaTime * MSpeed;
        if(Horizontal == 0)
        {
            anim.SetBool("Wolkbool", false);
        }
        else if(Horizontal > 0)
        {
            anim.SetBool("Wolkbool",true);
            sprender.flipX = true;
        }
        else if(Horizontal < 0)
        {
            anim.SetBool("Wolkbool",true);
            sprender.flipX = false;
        }
        transform.position = Pos;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); 
        viewPos.x = Mathf.Clamp01(viewPos.x); 
        viewPos.y = Mathf.Clamp01(viewPos.y); 
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); 
        transform.position = worldPos; 
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(rigid2D.velocity.y == 0)
            {
                anim.SetTrigger("JumpTri");
                anim.SetBool("Jumpbool", true);
                rigid2D.AddForce(Vector3.up * JForce, ForceMode2D.Impulse);
            }
                
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            other.enabled = false;
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            anim.SetBool("Jumpbool", false);
        }
    }

    
}
