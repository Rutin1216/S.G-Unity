using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCtrl : MonoBehaviour
{
    private Rigidbody2D r2d;
    void Awake()
    {
        r2d = gameObject.GetComponent<Rigidbody2D>();
    }
}
