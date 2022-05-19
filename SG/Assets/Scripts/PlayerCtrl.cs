using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int MSpeed;
    public int JForce;
    Vector3 Pos;
    float Horizontal;
    Rigidbody2D rigid2D;
    private bool Jumpbool;
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Pos = transform.position;
        Pos.x += Horizontal * Time.deltaTime * MSpeed;
        transform.position = Pos;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Jumpbool)
            {
                Jumpbool = true;
                rigid2D.AddForce(Vector3.up * JForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Jumpbool = false;
        }
    }
}
