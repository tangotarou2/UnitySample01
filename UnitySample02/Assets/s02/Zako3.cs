using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zako3 : MonoBehaviour
{
    const float LEN = 10.0f;
    Vector3 p1;

    // Start is called before the first frame update
    void Start()
    {
        p1.x = LEN;
    }

    // Update is called once per frame
    void Update()
    {

    //三角関数ないのにサインカーブと同じ！
        var pos = transform.position;
        p1.x +=  -pos.x* Time.deltaTime;
     //     pos.x = Mathf.Sin(Time.time) *LEN;


        pos.x += p1.x * Time.deltaTime;

        transform.position = pos;
    }
}
