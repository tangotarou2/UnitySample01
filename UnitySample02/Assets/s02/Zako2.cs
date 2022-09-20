using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zako2 : MonoBehaviour
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
        var pos = transform.position;
        p1.x +=  -Mathf.Sin(Time.time) *LEN * Time.deltaTime;
        pos.x += p1.x * Time.deltaTime;

        transform.position = pos;
    }
}
