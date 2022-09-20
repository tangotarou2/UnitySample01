using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothoid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float t;
    float t1;

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        t1 += Time.deltaTime;
        t += t1 * Time.deltaTime;
        pos.x += Mathf.Cos(t)* 0.1f;
        pos.y += Mathf.Sin(t)* 0.1f;

        if (t> Mathf.PI*2f) {
            t = 0;
            t1 = 0;
            pos.x = 0;
            pos.y = 0;
        }

        transform.position = pos;

    }
}
