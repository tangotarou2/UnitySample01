using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bane : MonoBehaviour
{
    public Material[] m_mats;
    public Transform target;
    GameObject _target;
    void Start()
    {
        StartCoroutine("loop");
    }

    IEnumerator loop(){
        Vector3[] table = new Vector3[] {

            new Vector3( 4f,4,0),
            new Vector3( 8,0,0),
            new Vector3( 8,8,0),
            new Vector3( 0,8,0),
            new Vector3( -8,2,0),
            new Vector3( 6,-3,0),
        };
        int idx = 0;
        var _target = this;
        var mat = GetComponent<MeshRenderer>().material;
        for (; ;){
            var target = table[idx];
            if ( _target != null ){
                _target.transform.position = target;
            }
            mat.color = m_mats[(idx%3)].color;

            yield return new WaitForSeconds(3f);
            idx = (idx+1) % table.Length;

        }
        //yield return null;
    }

    Vector3 p1;
    void Update()
    {
        var pos = transform.position;
        p1 += (target.position - pos) *4f * Time.deltaTime; // 4 ÇÕÇŒÇÀíËêî
        p1 -= p1 * 1f * Time.deltaTime; // dragÅ@ë¨ìxî€íËíÔçR
        pos += p1 * Time.deltaTime;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(p1, Vector3.forward);
    }
}
