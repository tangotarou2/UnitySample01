using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float m_rotY = 0;
    float m_rotX = 0;


    // Update is called once per frame
    void Update()
    {
        float addvalY = 0.1f;
        float addvalX = 0.1f;
        var cam = Camera.main;
        if( Input.GetKey( KeyCode.UpArrow)){
            m_rotX -= addvalX;

        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            m_rotX += addvalX;

        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            m_rotY += addvalY;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            m_rotY -= addvalY;
        }

        //var rot = Quaternion.Euler((float)m_rotX, (float)m_rotY, 0);
        //cam.transform.localRotation = rot;

        var t_pos = new Vector3(m_rotX, 1, -10);

        var forward = cam.transform.localPosition * -1.0f;
        var rot = Quaternion.LookRotation(forward);
        //cam.transform.localRotation = rot;


        cam.transform.LookAt(Vector3.zero);

        cam.transform.localPosition = t_pos;

    }
}
