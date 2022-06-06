using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCtrl : MonoBehaviour
{
    private Vector3 pos, rpos;
    private Vector3 rot;
    private Collider2D c2d;
    private Vector3 Spown;

    // Start is called before the first frame update
    void Start()
    {
        c2d = gameObject.GetComponent<Collider2D>();
        c2d.enabled = false;
        pos = new Vector3(100f,0,0);
        rpos = new Vector3(-100f,0,0);
        rot = new Vector3();
        Spown = new Vector3(660,-280);
    }

    // Update is called once per frame
    void Update()
    {
        if(c2d.enabled == true)
        {
            if(transform.position.x >= 660)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = rpos;
            }
            else if(transform.position.x <= -660)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = pos;
            }
            rot.z += Time.deltaTime*50;
            transform.localEulerAngles = rot;
        }
        else
        {
            transform.position = Spown;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            c2d.enabled =false;
            rot.z = 0;
            transform.localEulerAngles = rot;
        }
    }
}
