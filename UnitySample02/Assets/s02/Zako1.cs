using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zako1 : MonoBehaviour
{

    const float LEN = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        //”÷•ª‚µ‚½‚à‚Ì‚ğÏ•ª‚·‚é‚ÆŒ³‚Æ‚¨‚È‚¶B

        pos.x += Mathf.Cos(Time.time) *LEN * Time.deltaTime;

        transform.position = pos;
    }
}
