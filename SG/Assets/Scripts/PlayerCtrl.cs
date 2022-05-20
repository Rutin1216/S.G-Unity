using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCtrl : MonoBehaviour
{
    public int MSpeed;
    public int JForce;
    public Text Score; //점수UI
    public GameObject GM;
    private bool scorebool;
    private Vector3 Pos;
    private float Horizontal;
    private float PlayerScore;
    private Rigidbody2D rigid2D;
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
        scorebool = GM.GetComponent<GameManagerCtrl>().Scorebool;
    }
    void FixedUpdate()
    {
        if(scorebool){
            StartCoroutine(GetScore());
            
        }
    }

    void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Pos = transform.position;
        Pos.x += Horizontal * Time.deltaTime * MSpeed;
        transform.position = Pos;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //ĳ������ ���� ��ǥ�� ����Ʈ ��ǥ��� ��ȯ���ش�.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //�ٽ� ���� ��ǥ�� ��ȯ�Ѵ�.
        transform.position = worldPos; //��ǥ�� �����Ѵ�.
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(rigid2D.velocity.y == 0)
                rigid2D.AddForce(Vector3.up * JForce, ForceMode2D.Impulse);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            other.enabled = false;
        }
    }

    IEnumerator GetScore()
    {
        PlayerScore += Time.deltaTime;
        Score.GetComponent<Text>().text = ((int)PlayerScore).ToString();
        yield return null;
    }
}
