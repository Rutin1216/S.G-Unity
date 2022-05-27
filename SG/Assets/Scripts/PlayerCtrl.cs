using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{
    public int MSpeed;
    public int JForce;
    public int C_score; // C학점 점수
    public int B_score; // B학점 점수
    public int A_score; // A학점 점수
    public int Aplus_socre; // A+학점 점수
    public GameObject Gamemanager;
    private Vector3 Pos;
    private float Horizontal;
    private Rigidbody2D rigid2D;
    private Animator anim;
    private SpriteRenderer sprender;
    private int life; //체력

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprender = GetComponent<SpriteRenderer>();
        Gamemanager = GameObject.Find("GameManager");
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
        if(rigid2D.velocity.y == 0)
        {
            anim.SetBool("Jumpbool", false);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            switch(other.gameObject.tag)
            {
                case "F" :
                    StartCoroutine(playerhit(other));
                    break;
                case "C":
                    StartCoroutine(getScore(other, C_score));
                    break;
                case "B":
                    StartCoroutine(getScore(other, B_score));
                    break;
                case "A":
                    StartCoroutine(getScore(other, A_score));
                    break;   
                case "Aplus":
                    StartCoroutine(getScore(other, Aplus_socre));
                    break; 
            }
        }
    }

    IEnumerator playerhit(Collider2D other)
    {
        other.enabled = false;
        gameObject.layer = 8;
        life--;
        int cnt = 0;
        while(cnt < 10)
        {
            if(cnt % 2 == 0)
                sprender.color = new Color32(255,255,255,90);
            else
                sprender.color = new Color32(255,255,255,180);

            yield return new WaitForSeconds(0.2f);
            cnt++;
        }
        gameObject.layer = 7;
        sprender.color = new Color32(255,255,255,255);
    }

    IEnumerator getScore(Collider2D other, int score)
    {
        Gamemanager.GetComponent<GameManagerCtrl>().addScore(score);
        other.enabled = false;
        yield return null;
    }

    void GameOver()
    {
        
    }
}
