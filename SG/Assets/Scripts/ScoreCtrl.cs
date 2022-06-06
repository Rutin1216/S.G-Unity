using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCtrl : MonoBehaviour
{
    private Rigidbody2D r2d;
    private Vector3 pos;
    void Start()
    {
        r2d = gameObject.GetComponent<Rigidbody2D>();
        pos = new Vector2(0,Random.Range(-100f, -200f));
    }
    void Update()
    {
        r2d.velocity = pos;
    }
    
}
