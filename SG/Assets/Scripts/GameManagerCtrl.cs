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
    public Text Score; //점수UI

    //학점오브젝트
    public GameObject Score_F;
    public int Score_F_Max;
    public int F_level; //난이도 설정
    public GameObject Score_C;
    public int Score_C_Max;
    public int C_level; //난이도 설정
    public GameObject Score_B;
    public int Score_B_Max;
    public int B_level; //난이도 설정
    public GameObject Score_A;
    public int Score_A_Max;
    public int A_level; //난이도 설정
    public GameObject Score_Aplus;
    public int Score_Aplus_Max;
    public int Aplus_level; //난이도 설정

    //중요! 건드리면 안됨
    private GameObject[] Score_F_Array;
    private GameObject[] Score_C_Array;
    private GameObject[] Score_B_Array;
    private GameObject[] Score_A_Array;
    private GameObject[] Score_Aplus_Array;
    private MemoryPool MPool_F, MPool_C, MPool_B, MPool_A, MPool_Aplus;
    private Vector3 Spownpoint;
    private float PlayerScore;
    private bool Puasebool;
    private bool Scorebool;
    private int Gamelevel;


    // Start is called before the first frame update
    void Start()
    {
        Scorebool = false;
        Puasebool = false;
        Gamelevel = 1;

        MPool_F = new MemoryPool();
        MPool_C = new MemoryPool();
        MPool_B = new MemoryPool();
        MPool_A = new MemoryPool();
        MPool_Aplus = new MemoryPool();

        MPool_F.Create(Score_F, Score_F_Max);
        Score_F_Array = new GameObject[Score_F_Max];
        MPool_C.Create(Score_C, Score_C_Max);
        Score_C_Array = new GameObject[Score_C_Max];
        MPool_B.Create(Score_B, Score_B_Max);
        Score_B_Array = new GameObject[Score_B_Max];
        MPool_A.Create(Score_A, Score_A_Max);
        Score_A_Array = new GameObject[Score_A_Max];
        MPool_Aplus.Create(Score_Aplus, Score_Aplus_Max);
        Score_Aplus_Array = new GameObject[Score_Aplus_Max];

        Spownpoint = new Vector3(0, 370.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Scorebool)
        {
            StartCoroutine(GetScore());
            ScoreGenerator();
        }
    }

    void FixedUpdate()
    {
        
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
        //F학점 오브젝트 생성기
        for(int i = 0; i < F_level; i++)
        { 
            if (Score_F_Array[i] == null) 
            { 
                Score_F_Array[i] = MPool_F.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_F_Array[i].transform.position = Spownpoint;

                break;
            } 
        }
        for(int i = 0; i < F_level; i++)
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

        //C학점 오브젝트 생성기
        for(int i = 0; i < C_level; i++)
        { 
            if (Score_C_Array[i] == null) 
            { 
                Score_C_Array[i] = MPool_C.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_C_Array[i].transform.position = Spownpoint;
                
                break;
            } 
        }
        for(int i = 0; i < C_level; i++)
        {
            // 만약 미사일[i]가 활성화 되어있다면
            if(Score_C_Array[i])
            {
                // 미사일[i]의 Collider2D가 비활성 되었다면
                if(Score_C_Array[i].GetComponent<Collider2D>().enabled == false)
                {
                    // 다시 Collider2D를 활성화 시키고
                    Score_C_Array[i].GetComponent<Collider2D>().enabled = true;
                    // 미사일을 메모리로 돌려보내고
                    MPool_C.RemoveItem(Score_C_Array[i]);
                    // 가리키는 배열의 해당 항목도 null(값 없음)로 만든다.
                    Score_C_Array[i] = null;
                }
            }
        }
        //B학점 오브젝트 생성기
        for(int i = 0; i < B_level; i++)
        { 
            if (Score_B_Array[i] == null) 
            { 
                Score_B_Array[i] = MPool_B.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_B_Array[i].transform.position = Spownpoint;
                
                break;
            } 
        }
        for(int i = 0; i < B_level; i++)
        {
            // 만약 미사일[i]가 활성화 되어있다면
            if(Score_B_Array[i])
            {
                // 미사일[i]의 Collider2D가 비활성 되었다면
                if(Score_B_Array[i].GetComponent<Collider2D>().enabled == false)
                {
                    // 다시 Collider2D를 활성화 시키고
                    Score_B_Array[i].GetComponent<Collider2D>().enabled = true;
                    // 미사일을 메모리로 돌려보내고
                    MPool_B.RemoveItem(Score_B_Array[i]);
                    // 가리키는 배열의 해당 항목도 null(값 없음)로 만든다.
                    Score_B_Array[i] = null;
                }
            }
        }

        //A학점 오브젝트 생성기
        for(int i = 0; i < A_level; i++)
        { 
            if (Score_A_Array[i] == null) 
            { 
                Score_A_Array[i] = MPool_A.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_A_Array[i].transform.position = Spownpoint;
                
                break;
            } 
        }
        for(int i = 0; i < A_level; i++)
        {
            // 만약 미사일[i]가 활성화 되어있다면
            if(Score_A_Array[i])
            {
                // 미사일[i]의 Collider2D가 비활성 되었다면
                if(Score_A_Array[i].GetComponent<Collider2D>().enabled == false)
                {
                    // 다시 Collider2D를 활성화 시키고
                    Score_A_Array[i].GetComponent<Collider2D>().enabled = true;
                    // 미사일을 메모리로 돌려보내고
                    MPool_A.RemoveItem(Score_A_Array[i]);
                    // 가리키는 배열의 해당 항목도 null(값 없음)로 만든다.
                    Score_A_Array[i] = null;
                }
            }
        }

        //Aplus학점 오브젝트 생성기
        for(int i = 0; i < Aplus_level; i++)
        { 
            if (Score_Aplus_Array[i] == null) 
            { 
                Score_Aplus_Array[i] = MPool_Aplus.NewItem(); 

                Spownpoint.x = Random.Range(-640.0f,640.0f);
                
                Score_Aplus_Array[i].transform.position = Spownpoint;
                
                break;
            } 
        }
        for(int i = 0; i < Aplus_level; i++)
        {
            // 만약 미사일[i]가 활성화 되어있다면
            if(Score_Aplus_Array[i])
            {
                // 미사일[i]의 Collider2D가 비활성 되었다면
                if(Score_Aplus_Array[i].GetComponent<Collider2D>().enabled == false)
                {
                    // 다시 Collider2D를 활성화 시키고
                    Score_Aplus_Array[i].GetComponent<Collider2D>().enabled = true;
                    // 미사일을 메모리로 돌려보내고
                    MPool_Aplus.RemoveItem(Score_Aplus_Array[i]);
                    // 가리키는 배열의 해당 항목도 null(값 없음)로 만든다.
                    Score_Aplus_Array[i] = null;
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
                StartCanvas.enabled = false;
            }
            yield return null;
        }
        
    }

    IEnumerator GetScore()
    {
        PlayerScore += Time.deltaTime;
        
        if((int)PlayerScore >= 300)
        {
            Gamelevel = 4;
        }
        else if((int)PlayerScore >= 200)
        {
            Gamelevel = 3;
        }
        else if((int)PlayerScore >= 100)
        {
            Gamelevel = 2;
        }
        Score.GetComponent<Text>().text = ((int)PlayerScore).ToString();
        StartCoroutine(Gamelevelup());
        yield return null;
    }
    IEnumerator Gamelevelup()
    {
        switch(Gamelevel)
        {
            case 2 :
                F_level = 7;
                C_level = 5;
                B_level = 4;
                A_level = 2;
                break;
            case 3 :
                F_level = 9;
                C_level = 7;
                B_level = 5;
                A_level = 3;
                break;
            case 4 :
                F_level = 10;
                C_level = 7;
                B_level = 6;
                A_level = 4;
                break;
        }
        yield return null;
    }

    public void addScore(int score)
    {
        PlayerScore += score;
        Score.GetComponent<Text>().text = ((int)PlayerScore).ToString();
    }

}
