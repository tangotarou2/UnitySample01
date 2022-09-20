using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{

    public class CameraController : MonoBehaviour
    {
        public Transform m_target;



        float m_rotY = 0;
        float m_rotX = 0;


        //回転させるスピード
        public float rotateSpeed = 0.1f;

        Vector3 init_pos;
        Quaternion init_rot;
        private void Start()
        {
            init_pos = this.transform.localPosition;
            init_rot = this.transform.localRotation;


        }

        void ResetCamera(){
            this.transform.localPosition = init_pos;
            this.transform.localRotation = init_rot;

        }

        // Update is called once per frame
        void UpdateCamera()
        {
            var cam = Camera.main;

            float addvalY = 0.1f;
            float addvalX = 0.1f;

            m_rotY = 0;
            m_rotX = 0;


            //float angle = Input.GetAxis("Horizontal") * rotateSpeed;
            //float angleV = Input.GetAxis("Vertical") * rotateSpeed;

            if (Input.GetKey(KeyCode.RightArrow)) {
                m_rotY += addvalY;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                m_rotY -= addvalY;
            }

            if (Input.GetKey(KeyCode.UpArrow)) {
                m_rotX -= addvalX;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                m_rotX += addvalX;

            }
            if (Input.GetKey(KeyCode.R)) {
                ResetCamera();
            }


            var m_targetPos = m_target.transform.localPosition;
                float zoomspeed = 0.1f;
            if (Input.GetKey(KeyCode.PageUp)) {
                Vector3 t_pos = cam.transform.localPosition;
                Vector3 dir = (t_pos - m_targetPos);
                t_pos -= dir.normalized*zoomspeed; ;
                cam.transform.localPosition = t_pos;
            }
            if (Input.GetKey(KeyCode.PageDown)) {
                Vector3 t_pos = cam.transform.localPosition;
                Vector3 dir = (t_pos - m_targetPos);
                t_pos += dir.normalized*zoomspeed;
                cam.transform.localPosition = t_pos;
            }

            //カメラを回転させる
            cam.transform.RotateAround(m_targetPos, Vector3.up, m_rotY);
            cam.transform.RotateAround(m_targetPos, Vector3.right, m_rotX);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCamera();
        }

        void test()
        {

            float addvalY = 0.1f;
            float addvalX = 0.1f;
            var cam = Camera.main;
            if (Input.GetKey(KeyCode.UpArrow)) {
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



}