using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerCtrl : MonoBehaviour
{
    public float time;//페이드아웃 시간 설정
    public Image Logo;//게임로고UI
    public Button Start_BT;//게임시작 버튼UI
    public Button Setting_BT;//게임설정 버튼UI
    public Canvas StartCanvas;//게임시작UI

    //학점오브젝트
    public GameObject Score_F;
    public int Score_F_Max;
    public GameObject Score_C;
    public int Score_C_Max;
    public GameObject Score_B;
    public int Score_B_Max;
    public GameObject Score_A;
    public int Score_A_Max;
    public bool Scorebool;

    //중요! 건드리면 안됨
    private GameObject[] Score_F_Array;
    private GameObject[] Score_C_Array;
    private GameObject[] Score_B_Array;
    private GameObject[] Score_A_Array;
    private MemoryPool MPool_F, MPool_C, MPool_B, MPool_A;
    private Vector3 Spownpoint;
    private bool Puasebool;
    

    // Start is called before the first frame update
    void Start()
    {
        Scorebool = false;
        Puasebool = false;

        MPool_F = new MemoryPool();
        MPool_C = new MemoryPool();
        MPool_B = new MemoryPool();
        MPool_A = new MemoryPool();

        MPool_F.Create(Score_F, Score_F_Max);
        Score_F_Array = new GameObject[Score_F_Max];
        MPool_C.Create(Score_C, Score_C_Max);
        Score_C_Array = new GameObject[Score_C_Max];
        MPool_B.Create(Score_B, Score_B_Max);
        Score_B_Array = new GameObject[Score_B_Max];
        MPool_A.Create(Score_A, Score_A_Max);
        Score_A_Array = new GameObject[Score_A_Max];
        Spownpoint = new Vector3(0, 370.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(Scorebool)
        {
            ScoreGenerator();
        }
    }
    public void StartBT()
    {
        StartCoroutine(FadeOut(Logo));
        StartCoroutine(FadeOut(Start_BT.GetComponent<Image>()));
        StartCoroutine(FadeOut(Setting_BT.GetComponent<Image>()));
        Debug.Log("click");

    }
    public void Pause()
    {
        if(!Puasebool)
        {
            Time.timeScale = 0;
            Puasebool =true;
        }   
        else
        {
            Time.timeScale = 1.0f;
            Puasebool = false;
        }
            
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
    }

    private void ScoreGenerator()
    {
        for(int i = 0; i < Score_F_Max; i++)
        { 
            if (Score_F_Array[i] == null) 
            { 
                Score_F_Array[i] = MPool_F.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_F_Array[i].transform.position = Spownpoint;
                
                break;
            } 
        }

        for(int i = 0; i < Score_F_Max; i++)
        {
            // 만약 미사일[i]가 활성화 되어있다면
            if(Score_F_Array[i])
            {
                // 미사일[i]의 Collider2D가 비활성 되었다면
                if(Score_F_Array[i].GetComponent<Collider2D>().enabled == false)
                {
                    // 다시 Collider2D를 활성화 시키고
                    Score_F_Array[i].GetComponent<Collider2D>().enabled = true;
                    // 미사일을 메모리로 돌려보내고
                    MPool_F.RemoveItem(Score_F_Array[i]);
                    // 가리키는 배열의 해당 항목도 null(값 없음)로 만든다.
                    Score_F_Array[i] = null;
                }
            }
        }
    }

    IEnumerator FadeOut(Image image)
    {
        Color color = image.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            image.color = color;
            if(color.a <= 0f){
                color.a = 0f;
                Scorebool = true;
                yield return 1f;
                Destroy(StartCanvas.gameObject);
            }
            yield return null;
        }
        
    }

    
}
